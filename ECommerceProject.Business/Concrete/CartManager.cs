using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Entities.Concrete;
using ECommerceProject.Entities.DomainModels;
using Microsoft.VisualBasic.CompilerServices;

namespace ECommerceProject.Business.Concrete
{
    public class CartManager : ICartService
    {

        public void AddToCart(Cart cart, Product product)
        {
            CartLine cartLine = cart.CartLines.FirstOrDefault(c => c.Product.ProductId == product.ProductId);
            if (cartLine!=null)
            {
                cartLine.Quantity++;
                
            }
            else
            {
                cart.CartLines.Add(new CartLine(){Product= product, Quantity = 1});
            }
        }

        public void RemoveFromCart(Cart cart, int productId)
        {
            cart.CartLines.Remove(cart.CartLines.FirstOrDefault(c => c.Product.ProductId == productId));
        }

        public double TotalPrice(Cart cart)
        {
            
            return cart.TotalPrice();
        }

        public int TotalProductQuantity(Cart cart)
        {
            return cart.TotalProductQuantity();
        }

        public List<CartLine> List(Cart cart)
        {
            return cart.CartLines;
        }
    }
}
