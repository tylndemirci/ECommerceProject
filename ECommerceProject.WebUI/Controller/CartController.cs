using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Entities;
using ECommerceProject.Entities.Concrete;
using ECommerceProject.WebUI.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebUI.Controller
{
    public class CartController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartSessionHelper _cartSessionHelper;
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ICartService cartService, ICartSessionHelper cartSessionHelper, IProductService productService, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _cartSessionHelper = cartSessionHelper;
            _productService = productService;
            _userManager = userManager;
        }
        //todo add tempdata
        public IActionResult AddToCart(int productId, string userId)
        {
            var user = _userManager.FindByIdAsync(userId);
            Product product = _productService.GetProduct(productId);
            var cart = _cartSessionHelper.GetCart("cart");
            cart.UserId = user.Result.Id;
            _cartService.AddToCart(cart, product);
            //tempdata
            _cartSessionHelper.SetCart("cart", cart);

            return RedirectToAction("Index", "Product");
        }

        public IActionResult RemoveFromCart(int productId, string userId)
        {
            var user = _userManager.FindByIdAsync(userId);
            Product product = _productService.GetProduct(productId);
            var cart = _cartSessionHelper.GetCart("cart");
            cart.UserId = user.Result.Id;
            _cartService.RemoveFromCart(cart,productId);
            //tempdata
            _cartSessionHelper.SetCart("cart", cart);

            return RedirectToAction("Index", "Cart");
        }







    }
}