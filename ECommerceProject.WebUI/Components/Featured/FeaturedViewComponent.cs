using System.Linq;
using ECommerceProject.Business.Abstract;
using ECommerceProject.WebUI.Models.ViewComponent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.WebUI.Components.Featured
{
    public class FeaturedViewComponent: ViewComponent
    {
        private readonly IProductService _productService;
        public FeaturedViewComponent(IProductService productService)
        {
            _productService = productService;
        }
            
        public IViewComponentResult Invoke()
        {
            
            var returnModel = _productService.ListProduct()
                .Where(x=>x.IsFeatured)
                .Include(x=> x.Category)
                .OrderByDescending(x=>x.ProductId)
                .Select(x=> new NewProductsViewModel(x)).ToList();
            
                return View(returnModel);
            
            

        }
    }
}
