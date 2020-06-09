using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Business.Abstract;
using ECommerceProject.WebUI.Models.ViewComponent.ProductFilter;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebUI.Components.ProductFilter
{
    public class ProductFilterForProductsViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ProductFilterForProductsViewComponent(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public IViewComponentResult Invoke(int categoryId)
        {

            if (categoryId == 0)
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
            if (checkParent != null)
            {
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
