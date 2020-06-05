using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Entities.Concrete;
using ECommerceProject.WebUI.Models.Category;
using ECommerceProject.WebUI.Models.ViewComponent.ProductFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace ECommerceProject.WebUI.Controller
{
    public class CategoryController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IProductService _productService;

        public CategoryController(IProductService productService)
        {
            _productService = productService;
        }
        //[Route("/Products")]
        //public IActionResult ListCategoryProducts(ProductFilterViewModel model, int categoryId, int pageIndex=1)
        //{
        //    ViewBag.CategoryId = categoryId;
        //    var minPrice = model.Min;
        //    var maxPrice = model.Max;
        //    var subCategoryId = model.SearchCategoryId;
        //    var getCategoryId = model.CategoryId;
        //    int pageSize = 5;
        //    int excludeRecords = (pageSize * pageIndex) - pageSize;
         
            



        //    if (minPrice> maxPrice)
        //    {
        //        ModelState.AddModelError("","Minimum price cannot bigger than maximum price");
        //        var products = _productService.ListProduct()
        //            .Where(x => x.CategoryId == categoryId || x.Category.ParentCategoryId == categoryId)
        //            .Skip(excludeRecords)
        //            .Take(pageSize)
        //            .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));
        //        var resultd = new PagedResult<ListCategoryProductsModel>
        //        {
        //            Data = products.ToList(),
        //            TotalItems = products.Count(),
        //            PageNumber = pageIndex,
        //            PageSize = pageSize
        //        };

        //        return View(resultd);
        //    }

        //    if (subCategoryId == 0 && minPrice == 0 && maxPrice == 0)
        //    {
        //        var products = _productService.ListProduct()
        //            .Where(x => x.CategoryId == categoryId || x.Category.ParentCategoryId == categoryId)
        //            .Skip(excludeRecords)
        //            .Take(pageSize)
        //            .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));
        //        var resultd = new PagedResult<ListCategoryProductsModel>
        //        {
        //            Data = products.ToList(),
        //            TotalItems = products.Count(),
        //            PageNumber = pageIndex,
        //            PageSize = pageSize
        //        };

        //        return View(resultd);
        //    }
           

        //    if (subCategoryId != 0 || minPrice>=0 || maxPrice==0)  
        //    {
        //        if (maxPrice==0)
        //        {
        //            var products = _productService.ListProduct()
        //                .Where(x => x.CategoryId == categoryId
        //                            || x.Category.ParentCategoryId == categoryId
        //                            && x.CategoryId == subCategoryId
        //                            && x.Price >= minPrice)
        //                .Skip(excludeRecords)
        //                .Take(pageSize)
        //                .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));
        //            var resultd = new PagedResult<ListCategoryProductsModel>
        //            {
        //                Data = products.ToList(),
        //                TotalItems = products.Count(),
        //                PageNumber = pageIndex,
        //                PageSize = pageSize
        //            };

        //            return View(resultd);


        //        }

        //        var getMaxPrice = _productService.ListProduct().Select(x => x.Price).Max();
        //        var returnProducts = _productService.ListProduct()
        //            .Where(x => x.CategoryId == categoryId
        //                        || x.Category.ParentCategoryId == categoryId
        //                        && x.CategoryId == subCategoryId
        //                        && x.Price >= minPrice && x.Price <= getMaxPrice)
        //            .Skip(excludeRecords)
        //            .Take(pageSize)
        //            .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));

        //        var result = new PagedResult<ListCategoryProductsModel>
        //        {
        //            Data= returnProducts.ToList(),
        //            TotalItems = returnProducts.Count(),
        //            PageNumber = pageIndex,
        //            PageSize = pageSize
        //        };
              
        //        return View(result);
               
               
        //    }

        //    return RedirectToAction("Index", "Home");





        //}


        //public IActionResult ProductFilter(MiniProductFilterModel model)
        //{
        //    return RedirectToAction("ListCategoryProducts", new {min = model.Min, max = model.Max, categoryId = model.CategoryId});


        //}
    }
}
