using DepoOtomasyonu.DataAccess.Data;
using DepoOtomasyonu.Model.Models;
using DepoOtomasyonu.Model.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DepoOtomasyonu.Controllers
{
    public class AdminController : Controller
    {
        public readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(UserModel userModel)
        {
            List<UserModel> users = _context.Users.ToList();
            var userStatus = users.FirstOrDefault(x => x.UserName.Equals(userModel.UserName) && x.UserPassworrd.Equals(userModel.UserPassworrd));
            if (userStatus != null)
            {

                return RedirectToAction("AdminDashboard", "Admin");
            }
            else
            {
                return RedirectToAction("AdminLoginErrorPage", "Admin");
            }
        }
        public IActionResult AdminLoginErrorPage()
        {
            return View();
        }
        public IActionResult AdminDashboard()
        {
            return View();
        }
        public IActionResult CategoryDashboard()
        {
            var category = _context.Categories.ToList();
            return View(category);
        }
        #region category


        public IActionResult CategoryIndex()
        {
            var category = _context.Categories.ToList();
            return View(category);
        }
        public IActionResult CategoryInsertOrUpdate(int? id)
        {
            var category = _context.Categories.ToList();
            CategoryModel categoryModel = new CategoryModel();
            if (id == null)
            {
                return View(categoryModel);
            }
            categoryModel = category.FirstOrDefault(x => x.CategoryId == id);
            if (categoryModel == null)
            {
                return NotFound();
            }
            return View(categoryModel);
        }
        [HttpPost]
        public IActionResult CategoryInsertOrUpdate(CategoryModel categoryModel)
        {
            if (categoryModel.CategoryId == 0)
            {
                _context.Categories.Add(categoryModel);
                _context.SaveChanges();
            }
            else
            {
                _context.Categories.Update(categoryModel);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(CategoryIndex));
        }
        public IActionResult CategoryDelete(int id)
        {
            var category = _context.Categories.ToList();
            var categoryModel = category.FirstOrDefault(x => x.CategoryId == id);
            _context.Categories.Remove(categoryModel);
            _context.SaveChanges();
            return RedirectToAction(nameof(CategoryIndex));
        }
        #endregion
        #region Product


        public IActionResult ProductIndex()
        {
            var product = _context.Products.Include(x=>x.CategoryModel).ToList();
            return View(product);
        }
        public IActionResult ProductInsertOrUpdate(int? id)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            var products = _context.Products.ToList();
            var category = _context.Categories.ToList();
            productViewModel.CategoryList = category.Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.CategoryId.ToString(),
            });
            if (id == null)
            {
                return View(productViewModel);
            }
            productViewModel.ProductModel = products.FirstOrDefault(x => x.ProductId == id);
            if (productViewModel == null)
            {
                return NotFound();
            }
            return View(productViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ProductInsertOrUpdate(ProductViewModel productViewModel)
        {
            var files = HttpContext.Request.Form.Files;
            var dosya = files[0];
            string imageExtension = Path.GetExtension(dosya.FileName);
            string filepath = Path.GetFullPath(dosya.FileName);       
            string imageName = Guid.NewGuid() + imageExtension;
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{imageName}");
            using var stream = new FileStream(path, FileMode.Create);
            dosya.CopyToAsync(stream);
            productViewModel.ProductModel.ProductPicture = imageName;
            if (productViewModel.ProductModel.ProductId == 0)
            {
                _context.Products.Add(productViewModel.ProductModel);
                _context.SaveChanges();
            }
            else
            {
                _context.Products.Update(productViewModel.ProductModel);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(ProductIndex));
        }
        public IActionResult ProductDelete(int id)
        {
            var product = _context.Products.ToList();
            var productModel = product.FirstOrDefault(x => x.ProductId == id);
            _context.Products.Remove(productModel);
            _context.SaveChanges();
            return RedirectToAction(nameof(ProductIndex));
        }
        #endregion
    }
}
