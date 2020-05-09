using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Entities.Concrete;
using ECommerceProject.WebUI.Helper;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebUI.Controller
{
    public class CartController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartSessionHelper _cartSessionHelper;
        private readonly IProductService _productService;

        public CartController(ICartService cartService, ICartSessionHelper cartSessionHelper, IProductService productService)
        {
            _cartService = cartService;
            _cartSessionHelper = cartSessionHelper;
            _productService = productService;
        }
        //todo add tempdata
        public IActionResult AddtoCart(int productId)
        {
            Product product = _productService.GetProduct(productId);
            var cart = _cartSessionHelper.GetCart("cart");
            _cartService.AddToCart(cart, product);
            //tempdata
            _cartSessionHelper.SetCart("cart", cart);

            return RedirectToAction("Index", "Product");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            Product product = _productService.GetProduct(productId);
            var cart = _cartSessionHelper.GetCart("cart");
            _cartService.RemoveFromCart(cart,productId);
            //tempdata
            _cartSessionHelper.SetCart("cart", cart);

            return RedirectToAction("Index", "Cart");
        }







    }
}