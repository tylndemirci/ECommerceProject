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

            if (subCategoryId!= 0)
            {
                return View(
                        _productService.ListProduct()
                            .Where(x => x.CategoryId == categoryId || x.Category.ParentCategoryId == categoryId && x.CategoryId== subCategoryId)
                            .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x))
                    );
               
            }
            //if (min != null && max != null)
            //{
            //    var minMaxPriceProducts = _productService.ListProduct().Where(x => x.CategoryId == categoryId && x.Price >= min && x.Price <= max)
            //        .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));
            //    return View(minMaxPriceProducts);
            //}

            //if (min != null)
            //{
            //    var minPriceProducts = _productService.ListProduct().Where(x => x.CategoryId == categoryId && x.Price >= min)
            //        .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));
            //    return View(minPriceProducts);
            //}

            //if (max != null)
            //{
            //    var maxPriceProducts = _productService.ListProduct().Where(x => x.CategoryId == categoryId && x.Price <= max)
            //        .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));
            //    return View(maxPriceProducts);
            //}

            var products = _productService.ListProduct()
                .Where(x => x.CategoryId == categoryId || x.Category.ParentCategoryId==categoryId)
                .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));



           



            return View(products);
        }

        //public IActionResult ProductFilter(MiniProductFilterModel model)
        //{
        //    return RedirectToAction("ListCategoryProducts", new {min = model.Min, max = model.Max, categoryId = model.CategoryId});


        //}
    }
}
