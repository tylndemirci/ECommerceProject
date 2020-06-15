using System.Collections.Generic;
using System.Linq;

namespace ECommerceProject.Entities.DomainModels
{
   public class Cart
    {
        public Cart()
        {
            CartLines = new List<CartLine>();
        }

        public List<CartLine> CartLines { get; set; }
        public string UserId { get; set; }

        public double TotalPrice()
        {
            return CartLines.Sum(i => i.Product.Price * i.Quantity);
        }

        public int TotalProductQuantity()
        {
            return CartLines.Sum(x => x.Quantity);
        }
    }
}
