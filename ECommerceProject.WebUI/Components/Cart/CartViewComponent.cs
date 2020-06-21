using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ECommerceProject.Business.Abstract;
using ECommerceProject.WebUI.Helper;
using ECommerceProject.WebUI.Models.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebUI.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartSessionHelper _cartSessionHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductService _productService;


        public CartViewComponent(ICartSessionHelper cartSessionHelper, IHttpContextAccessor httpContextAccessor, IProductService productService)
        {
            _cartSessionHelper = cartSessionHelper;
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            var cart = _cartSessionHelper.GetCart("cart");
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            

            var returnModel = new CartModel(userId);
          
            foreach (var cartLine in cart.CartLines)
            {
                returnModel.ProductName.Add(cartLine.Product.ProductName);
                returnModel.ImageUrl.Add(cartLine.Product.ImageUrl);
                returnModel.Price.Add(cartLine.Product.Price);
                returnModel.Quantity.Add(cartLine.Quantity);
              
            }
            returnModel.TotalProductQuantity = cart.TotalProductQuantity();
            returnModel.TotalPrice = cart.TotalPrice();
            


                return View(returnModel);
        }
    }
}