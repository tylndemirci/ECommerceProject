using System.Linq;
using ECommerceProject.Business.Abstract;
using ECommerceProject.WebUI.Models.ViewComponent;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebUI.Components.Carousel
{
    public class CarouselViewComponent: ViewComponent
    {
        private readonly IProductService _productService;

        public CarouselViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            var returnModel = _productService.ListProduct().Where(x => x.IsApproved == true).Select(x=> new NewProductsViewModel(x));
            return View(returnModel);
        }
    }
}
