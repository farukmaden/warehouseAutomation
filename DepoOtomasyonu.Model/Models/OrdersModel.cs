using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepoOtomasyonu.Model.Models
{
    public class OrdersModel
    {
        public int OrdeId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public CustomerModel CustomerModel { get; set; }
        public int ProductId { get; set; }
        public ProductModel ProductModel { get; set; }
    }
}
