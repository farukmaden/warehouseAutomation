using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepoOtomasyonu.Model.Models
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPassword { get; set; }
        public List<CardModel> CardModels { get; set; }
        public List<ProductModel> ProductModels { get; set; }
        public List<OrdersModel> OrdersModels { get; set; }
    }
}
