using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.AdminUI.Models.Order
{
    public class ShowOrderLinesViewModel
    {
        public ShowOrderLinesViewModel(OrderLine orderLine)
        {
            OrderLineId = orderLine.OrderLineId;
            OrderId = orderLine.OrderId;
            ProductName = orderLine.Product.ProductName;
            Quantity = orderLine.Quantity;
            Price = orderLine.Price;
        }

        public ShowOrderLinesViewModel()
        {
            
        }
        public int OrderLineId { get; set; }
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
