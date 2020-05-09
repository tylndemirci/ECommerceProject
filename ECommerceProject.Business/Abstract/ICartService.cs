using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Entities.Concrete;
using ECommerceProject.Entities.DomainModels;

namespace ECommerceProject.Business.Abstract
{
   public interface ICartService
   {
       void AddToCart(Cart cart, Product product);
       void RemoveFromCart(Cart cart, int productId);
       List<CartLine> List(Cart cart);
   }
}
