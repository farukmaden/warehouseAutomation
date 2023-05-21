using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepoOtomasyonu.Model.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public int ProductCount { get; set; }
        public double ProductPrice { get; set; }
        public string ProductPicture { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel CategoryModel { get; set; }
        public List<CardModel> CardModels { get; set; }
        public List<OrdersModel> OrdersModels { get; set; }
    }
}
