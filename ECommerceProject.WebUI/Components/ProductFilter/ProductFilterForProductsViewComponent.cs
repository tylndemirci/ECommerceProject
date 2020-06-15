using System.Linq;
using ECommerceProject.Business.Abstract;
using ECommerceProject.WebUI.Models.ViewComponent.ProductFilter;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebUI.Components.ProductFilter
{
    public class ProductFilterForProductsViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public ProductFilterForProductsViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke(int categoryId)
        {
            if (categoryId == 0)
            {
                var innerSubCategory = new ProductFilterViewModel(_categoryService.GetAllSubCategories());

                return View(innerSubCategory);
            }


            var checkParent = _categoryService.GetCategory(categoryId).ParentCategoryId;
            if (checkParent != null)
            {
                var customCategory = new ProductFilterViewModel(_categoryService.GetAllSubCategories()
                    .Where(x => x.ParentCategoryId == checkParent.Value));
                customCategory.PageIndex = 1;
                customCategory.CategoryId = categoryId;
                customCategory.SearchCategoryName = _categoryService.GetCategory(categoryId).Title;
                customCategory.SearchCategoryId = categoryId;
                //ok
                return View(customCategory);
            }

            ViewData["CategoryId"] = categoryId;
            var subCategory = new ProductFilterViewModel(_categoryService.GetSubCategories(categoryId));
            subCategory.PageIndex = 1;
            subCategory.SearchCategoryId = categoryId;
            //subCategory.SearchCategoryName = _categoryService.GetCategory(categoryId).Title;
            //subCategory.SearchCategoryId = categoryId;


            //ok

            return View(subCategory);
        }
    }
}