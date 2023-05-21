using DepoOtomasyonu.DataAccess.Data;
using DepoOtomasyonu.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DepoOtomasyonu.Controllers
{
    public class CustomerController : Controller
    {
        public readonly ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult ListProduct(int id)
        {
            var products = _context.Products.Include(p => p.CategoryModel).ToList();
            var productList = products.Where(x => x.CategoryId == id && x.ProductCount > 0).ToList();
            var customer = GetSession("Customer");
            if (customer == null)
            {
                return RedirectToAction("LoginErrorPage", "Home");
            }
            return View(productList);
        }
        public IActionResult AddToCard(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
            var customer = GetSession("Customer");
            var customerId = Convert.ToInt32(customer);
            CardModel card = new CardModel()
            {
                ProductId = product.ProductId,
                CustomerId = customerId,
                ProductModel = product
            };
            _context.Cards.Add(card);
            _context.SaveChanges();
            return RedirectToAction(nameof(ListOfCard));
        }
        public IActionResult DeleteToCard(int id)
        {
            var card = _context.Cards.FirstOrDefault(x => x.CardId == id);
            _context.Cards.Remove(card);
            _context.SaveChanges();
            return RedirectToAction(nameof(ListOfCard));
        }
        public IActionResult ListOfCard()
        {
            var customer = GetSession("Customer");
            var customerId = Convert.ToInt32(customer);
            var card = _context.Cards.Include(x => x.ProductModel.CategoryModel).ToList();
            var customerCard = card.Where(x => x.CustomerId == customerId).ToList();
            if (customer == null)
            {
                return RedirectToAction("LoginErrorPage", "Home");
            }
            return View(customerCard);
        }
        public IActionResult AddToOrders(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
            var customer = GetSession("Customer");
            var customerId = Convert.ToInt32(customer);
            OrdersModel ordersModel = new OrdersModel()
            {
                ProductId = product.ProductId,
                CustomerId = customerId,
                ProductModel = product,
                OrderDate = DateTime.UtcNow
            };
            _context.Orders.Add(ordersModel);
            var card = _context.Cards.FirstOrDefault(x => x.ProductId == id);
            _context.Cards.Remove(card);
            product.ProductCount = product.ProductCount - 1;
            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(ListOfOrders));
        }
        public IActionResult ListOfOrders()
        {
            var orders = _context.Orders.Include(x => x.ProductModel.CategoryModel).ToList();
            var customer = GetSession("Customer");
            var customerId = Convert.ToInt32(customer);
            var customerOrders = orders.Where(x => x.CustomerId == customerId).ToList();
            if (customer == null)
            {
                return RedirectToAction("LoginErrorPage", "Home");
            }
            return View(customerOrders);
        }
        public string GetSession(string key)
        {
            return HttpContext.Session.GetString(key);
        }
    }
}
