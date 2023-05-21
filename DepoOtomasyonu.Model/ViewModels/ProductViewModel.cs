using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DepoOtomasyonu.Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DepoOtomasyonu.Model.ViewModels
{
    public class ProductViewModel
    {
        public ProductModel ProductModel { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
