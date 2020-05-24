using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Entities;
using ECommerceProject.WebUI.Extensions;
using ECommerceProject.WebUI.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ECommerceProject.WebUI.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartSessionHelper _cartSessionHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CartViewComponent(ICartSessionHelper cartSessionHelper, IHttpContextAccessor httpContextAccessor)
        {
            _cartSessionHelper = cartSessionHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            var cart = _cartSessionHelper.GetCart("cart");
            
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View();






        }



    }
}

