using System.Linq;
using ECommerceProject.Business.Abstract;
using ECommerceProject.WebUI.Models.ViewComponent.ProductFilter;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebUI.Components.ProductFilter
{
    public class ProductFilterViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ProductFilterViewComponent(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public IViewComponentResult Invoke(int categoryId)
        {

            var subCategory = _categoryService.GetSubCategories(categoryId)
                .Select(x => new ProductFilterViewModel(x));

          
           
                return View(subCategory);

                



        }
    }
}
