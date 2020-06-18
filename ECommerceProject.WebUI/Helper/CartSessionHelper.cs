using ECommerceProject.Core.CryptionFactory;
using ECommerceProject.Entities.DomainModels;
using ECommerceProject.WebUI.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ECommerceProject.WebUI.Helper
{
    public class CartSessionHelper: ICartSessionHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

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
        public Cart GetCartCookie(string key)
        {
            // Cart cartToCheck = _httpContextAccessor.HttpContext.Session.GetObject<Cart>(key);
            var cookieValue = "dsadasdasdsadas";
            var decryptedValue = cookieValue.Decrypt();
            var cartToCheck=JsonConvert.DeserializeObject<Cart>(decryptedValue);
                 
            if (cartToCheck==null)
            {
                cartToCheck = new Cart();
                SetCartCookie(key, cartToCheck);
                
                return cartToCheck;
            }

            return cartToCheck;
        }

        public void SetCart(string key, Cart cart)
        {
          _httpContextAccessor.HttpContext.Session.SetObject(key, cart);
        }

        public void SetCartCookie(string key, Cart cart)
        {
            var cookieValue = JsonConvert.SerializeObject(cart);
            var encryptedValue = cookieValue.Encrypt();
            // encryptedValue değerini Cookie üzerindeki CartContainer alanına basacak method;

            // _httpContextAccessor.HttpContext.Session.SetObject(key, cart);
        }

        public void Clear()
        {
           _httpContextAccessor.HttpContext.Session.Clear();
        }
    }
}
