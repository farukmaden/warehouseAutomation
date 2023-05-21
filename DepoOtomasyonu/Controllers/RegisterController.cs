using DepoOtomasyonu.DataAccess.Data;
using DepoOtomasyonu.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DepoOtomasyonu.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index() { return View(); }
        public IActionResult Register(CustomerModel customerModel)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.CustomerName == customerModel.CustomerName && x.CustomerPassword == customerModel.CustomerPassword);
            if (customer == null) 
            {
                _context.Customers.Add(customerModel);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("RegisterErrorPage", "Register");
        }
        public IActionResult RegisterErrorPage()
        {
            return View();
        }
    }
}
