using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.Entities.DomainModels
{
  public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        
    }
}
