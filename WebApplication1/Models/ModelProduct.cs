using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ModelProduct
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public int? productCategory { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string Details { get; set; }
        public string categoryName { get; set; }
    }
}