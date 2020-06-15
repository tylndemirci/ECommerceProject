using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebUI.Components
{
    public class WishListViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
