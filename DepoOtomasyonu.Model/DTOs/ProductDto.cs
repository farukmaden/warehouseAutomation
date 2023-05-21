using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepoOtomasyonu.Model.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public int ProductCount { get; set; }
        public double ProductPrice { get; set; }
        public IFormFile ProductPicture { get; set; }
        public int CategoryId { get; set; }
    }
}
