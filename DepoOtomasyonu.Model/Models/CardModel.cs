using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepoOtomasyonu.Model.Models
{
    public class CardModel
    {
        public int CardId { get; set; }
        public int CustomerId { get; set; }
        public CustomerModel CustomerModel { get; set; }
        public int ProductId { get; set; }
        public ProductModel ProductModel { get; set; }
    }
}
