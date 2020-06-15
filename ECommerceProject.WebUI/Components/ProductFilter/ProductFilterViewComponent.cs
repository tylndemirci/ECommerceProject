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

        public IViewComponentResult Invoke(int categoryId, string? searchFor)
        {
           
            if (categoryId==0)
            {
                var innerSubCategory = new ProductFilterViewModel(_categoryService.GetAllSubCategories());
                
                return View(innerSubCategory);

            }

           
            var checkParent = _categoryService.GetCategory(categoryId).ParentCategoryId;
            var customCategory = new ProductFilterViewModel(_categoryService.GetAllSubCategories().Where(x => x.ParentCategoryId == checkParent.Value));
            customCategory.PageIndex = 1;
            customCategory.CategoryId = categoryId;
            customCategory.SearchCategoryName = _categoryService.GetCategory(categoryId).Title;
            customCategory.SearchCategoryId = categoryId;
            if (checkParent!=null)
            {
                
                return View(customCategory);
            }
            ViewData["CategoryId"] = categoryId;
            var subCategory = new ProductFilterViewModel(_categoryService.GetSubCategories(categoryId));
            subCategory.PageIndex = 1;





            return View(subCategory);

                



        }
    }
}
