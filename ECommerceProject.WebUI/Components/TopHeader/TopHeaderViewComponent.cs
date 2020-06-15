using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebUI.Components
{
    public class TopHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
