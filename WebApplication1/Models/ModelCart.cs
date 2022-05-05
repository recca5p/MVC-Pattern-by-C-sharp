using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ModelCart
    {
        public int cartID { get; set; }
        public int productID { get; set; }
        public string productName { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
    }
}