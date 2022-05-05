using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ModelAccount
    {
        public string username { get; set; }
        public string password { get; set; }
        public string repassword { get; set; }
        public string role { get; set; }
    }
}