using System.Security.Claims;
using ECommerceProject.WebUI.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
 
            return View(cart);






        }



    }
}

