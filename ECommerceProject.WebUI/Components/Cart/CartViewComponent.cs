using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Business.Abstract;
using ECommerceProject.WebUI.Helper;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebUI.Components.Cart
{
    public class CartViewComponent: ViewComponent
    {
        private readonly ICartSessionHelper _cartSessionHelper;

        public CartViewComponent(ICartSessionHelper cartSessionHelper)
        {
            _cartSessionHelper = cartSessionHelper;
            
        }

        public IViewComponentResult Invoke()
        {
          var cart =   _cartSessionHelper.GetCart("cart") ?? new Entities.DomainModels.Cart();
            return View(cart);
        }
    }
}
