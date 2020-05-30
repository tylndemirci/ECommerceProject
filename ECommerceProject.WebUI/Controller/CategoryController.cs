using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Business.Abstract;
using ECommerceProject.WebUI.Models.Category;
using ECommerceProject.WebUI.Models.ViewComponent.ProductFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.WebUI.Controller
{
    public class CategoryController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IProductService _productService;

        public CategoryController(ICategoryService categoryService, IProductService productService)
        {
            _productService = productService;
        }
        [Route("/ProductList")]
        public IActionResult ListCategoryProducts(int categoryId, ProductFilterViewModel model)
        {
            ViewBag.CategoryId = categoryId;
            var minPrice = model.Min;
            var maxPrice = model.Max;
            var subCategoryId = model.SearchCategoryId;
            var getCategoryId = model.CategoryId;

            if (minPrice> maxPrice)
            {
                ModelState.AddModelError("","Minimum price cannot bigger than maximum price");
                var products = _productService.ListProduct()
                    .Where(x => x.CategoryId == categoryId || x.Category.ParentCategoryId == categoryId)
                    .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));

                return View(products);
            }

            if (subCategoryId == 0 && minPrice == 0 && maxPrice == 0)
            {
                var products = _productService.ListProduct()
                    .Where(x => x.CategoryId == categoryId || x.Category.ParentCategoryId == categoryId)
                    .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));

                return View(products);

            }
           

            if (subCategoryId != 0 || minPrice>=0 || maxPrice==0)  
            {
                if (maxPrice==0)
                {
                   
                   return View(
                       _productService.ListProduct()
                           .Where(x => x.CategoryId == categoryId  
                                       || x.Category.ParentCategoryId == categoryId
                                       && x.CategoryId == subCategoryId 
                                       && x.Price >= minPrice)
                           .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x))
                   );

                }

                var getMaxPrice = _productService.ListProduct().Select(x => x.Price).Max();
                return View(
                        _productService.ListProduct()
                            .Where(x => x.CategoryId == categoryId 
                                        || x.Category.ParentCategoryId == categoryId
                                        && x.CategoryId== subCategoryId
                                        && x.Price >= minPrice && x.Price<= getMaxPrice)
                            .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x))
                    );
               
            }

            return RedirectToAction("Index", "Home");





        }

        //public IActionResult ProductFilter(MiniProductFilterModel model)
        //{
        //    return RedirectToAction("ListCategoryProducts", new {min = model.Min, max = model.Max, categoryId = model.CategoryId});


        //}
    }
}
