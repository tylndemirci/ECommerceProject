using ECommerceProject.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ECommerceProject.Entities.Concrete
{
   public class Order
    {
       
        public int OrderId { get; set; }
        [Required] public string OrderNumber { get; set; }
        [Required] public double Total  { get; set; }
        [Required] public DateTime OrderDate { get; set; }

        [Required] public EnumOrderState OrderState { get; set; }
        [Required] public string? UserName { get; set; }

        [Required] public string Name { get; set; }
        [Required] public string Surname { get; set; }
        [Required] public string Address { get; set; }
        [Required] public string Country { get; set; }
        [Required] public string City { get; set; }
        [Required] public string District { get; set; }
        [Required] public string AddressTitle { get; set; }
        [Required] public string Phone { get; set; }
        public virtual List<OrderLine> OrderLines { get; set; }
    }
}
