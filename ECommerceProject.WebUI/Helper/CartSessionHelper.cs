using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Entities.DomainModels;
using ECommerceProject.WebUI.Extensions;
using Microsoft.AspNetCore.Http;

namespace ECommerceProject.WebUI.Helper
{
    public class CartSessionHelper: ICartSessionHelper
    {
        private IHttpContextAccessor _httpContextAccessor;

        public CartSessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Cart GetCart(string key)
        {
            Cart cartToCheck = _httpContextAccessor.HttpContext.Session.GetObject<Cart>(key);
            if (cartToCheck==null)
            {
                SetCart(key, new Cart());
                cartToCheck = _httpContextAccessor.HttpContext.Session.GetObject<Cart>(key);
                return cartToCheck;
            }

            return cartToCheck;
        }

        public void SetCart(string key, Cart cart)
        {
          _httpContextAccessor.HttpContext.Session.SetObject(key, cart);
        }

        public void Clear()
        {
           _httpContextAccessor.HttpContext.Session.Clear();
        }
    }
}
