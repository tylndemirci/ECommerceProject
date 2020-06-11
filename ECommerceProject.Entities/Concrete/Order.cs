using ECommerceProject.Core.Enums;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ECommerceProject.Entities.Concrete
{
   public class Order
    {
       
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public double Total  { get; set; }
        public DateTime OrderDate { get; set; }

        public EnumOrderState OrderState { get; set; }
        public string UserName { get; set; }
       
        public string Name { get; set; }
        public string Surname { get; set; } 
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string AddressTitle { get; set; }
        public string Phone { get; set; }
        public virtual List<OrderLine> OrderLines { get; set; }
    }
}
