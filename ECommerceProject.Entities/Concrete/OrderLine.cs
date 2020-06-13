using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ECommerceProject.Entities.Concrete
{
   public class OrderLine
    {
        public int OrderLineId { get; set; }
        [Required] public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        [Required] public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Required] public int Quantity { get; set; }
        [Required] public double Price { get; set; }
    }
}
