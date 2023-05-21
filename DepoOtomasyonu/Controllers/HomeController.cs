using DepoOtomasyonu.DataAccess.Data;
using DepoOtomasyonu.Model.Models;
using DepoOtomasyonu.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DepoOtomasyonu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(CustomerModel customerModel)
        {
            List<CustomerModel> customers = _context.Customers.ToList();
            var customerStatus = customers.FirstOrDefault(x => x.CustomerName.Equals(customerModel.CustomerName) && x.CustomerPassword.Equals(customerModel.CustomerPassword));
            if (customerStatus == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                SetSession("Customer", customerStatus.CustomerId.ToString());
                return RedirectToAction("CategoryListIndex", "Home");
            }
        }
        public IActionResult CategoryListIndex()
        {
            var category = _context.Categories.ToList();
            return View(category);
        }
        public IActionResult Exit()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public void SetSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }
        public string GetSession(string key)
        {
            return HttpContext.Session.GetString(key);
        }
        public IActionResult LoginErrorPage()
        {
            return View();
        }
    }
}
