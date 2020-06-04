using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Core.Enums;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.WebUI.Models.MyAccount
{
    public class MyOrdersViewModel
    {
        public List<int> OrderId { get; set; }
        public List<string> OrderNumber { get; set; }
        public List<string> OrderDate { get; set; }
        public List<string> OrderState { get; set; }
        public List<int> Quantity { get; set; }

        public List<string> ProductName { get; set; }
        public List<double> Price { get; set; }
        public List<double> Total { get; set; }
    }
}
