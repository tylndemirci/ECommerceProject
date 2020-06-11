using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

using cloudscribe.Pagination.Models;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Entities.Concrete;
using ECommerceProject.WebUI.Components.SearchBar;
using ECommerceProject.WebUI.Models.Category;
using ECommerceProject.WebUI.Models.Product;
using ECommerceProject.WebUI.Models.ViewComponent;
using ECommerceProject.WebUI.Models.ViewComponent.ProductFilter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace ECommerceProject.WebUI.Controller
{
    public class ProductController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductDetailsService _productDetailsService;

        public ProductController(IProductService productService, ICategoryService categoryService, IProductDetailsService productDetailsService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productDetailsService = productDetailsService;
        }
        [Route("/ProductDetails")]
        public IActionResult ProductDetails(int productId)
        {
            var subCategory = _categoryService.GetSubCategoryForProduct(productId);


            var getProduct = _productService.GetProduct(productId);
            getProduct.Category.ParentCategoryId = subCategory.ParentCategoryId;
            var getCategory = _categoryService.GetCategory(subCategory.ParentCategoryId ?? getProduct.CategoryId);

            _categoryService.GetCategory(getProduct.CategoryId);

            var setProduct = new ProductDetailsModel
            {
                ProductId = getProduct.ProductId,
                CategoryId = subCategory.ParentCategoryId ?? getProduct.CategoryId,
                CategoryName = getCategory.Title,
                SubCategoryName = subCategory.Title ?? getProduct.Category.Title,
                SubCategoryId = subCategory.Id,
                Count = getProduct.Count,
                Price = getProduct.Price,
                IsStock = getProduct.Count >= 1,
                ProductName = getProduct.ProductName,
                Description = getProduct.Description,
                ProductColor = getProduct.ProductColor,
                ImageUrl = getProduct.ImageUrl

            };

            var getDetails = _productDetailsService.GetAllDetails(productId).Where(x => x.IsDeleted == false);
            //Need to do the thing below to prevent setProduct.ProductDetails... returning null.
            setProduct.ProductDetailsTitle = new List<string>();
            setProduct.ProductDetailsDescription = new List<string>();
            foreach (var detail in getDetails)
            {

                setProduct.ProductDetailsTitle.Add(detail.ProductDetailTitle);
                setProduct.ProductDetailsDescription.Add(detail.ProductDetailDescription);

            }





            //var category = _categoryService.GetCategory(getProduct.CategoryId);

            return View(setProduct);
            //todo return to home in case something occurs.

        }

        //[Route("/Products-{pageIndex}-{categoryId}")]
        //[Route("/Products/{pageIndex}.{subCategoryId?}")]

        public IActionResult Products(ProductFilterViewModel model, int pageIndex = 1)
        {
            int pageSize = 6;
            int excludeRecords = (pageSize * pageIndex) - pageSize;

            ViewData["CategoryId"] = model.CategoryId;

            if (model.SearchCategoryId != 0)
            {
                
                //model.CategoryId = model.SearchCategoryId;
                ViewData["CategoryId"] = model.SearchCategoryId;
            }

            if (model.SearchCategoryId==0 && model.CategoryId!=0)
            {
                var getCategory = _categoryService.GetCategory(model.CategoryId);
                if (getCategory.ParentCategoryId!=null)
                {
                    model.SearchCategoryId = model.CategoryId;
                }
            }
            ViewData["MaxPrice"] = model.Max;
            if (model.Min > model.Max && (decimal)model.Max == 0)
            {
                model.Max = _productService.ListProduct().Select(x => x.Price).Max();
            }
            ViewData["MinPrice"] = model.Min;
            ViewData["SearchCategoryName"] = model.SearchCategoryName;


            if (model.Min > model.Max)
            {
                ModelState.AddModelError("", "Minimum price cannot bigger than maximum price");
                var products = _productService.ListProduct()
                    .Where(x => x.Category.ParentCategoryId == model.CategoryId || x.CategoryId == model.CategoryId)
                    .Skip(excludeRecords)
                    .Take(pageSize)
                    .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));

                var resultd = new PagedResult<ListCategoryProductsModel>
                {
                    Data = products.ToList(),
                    TotalItems = _productService.ListProduct().Count(x => x.Category.ParentCategoryId == model.CategoryId || x.CategoryId == model.CategoryId),
                    PageNumber = pageIndex,
                    PageSize = pageSize
                };
                //min>max
                return View(resultd);
            }


            if (model.SearchCategoryId == 0)
            {
                if (model.Min >= 0 && model.Max > 0)
                {
                    var products = _productService.ListProduct()
                        .Where(x =>x.Category.ParentCategoryId == model.CategoryId && x.Price >= model.Min && x.Price <= model.Max)
                        .Skip(excludeRecords)
                        .Take(pageSize)
                        .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));
                    var resultd = new PagedResult<ListCategoryProductsModel>
                    {
                        Data = products.ToList(),
                        TotalItems = _productService
                            .ListProduct().Count(x => x.Category.ParentCategoryId == model.CategoryId  && x.Price >= model.Min && x.Price <= model.Max),
                        PageNumber = pageIndex,
                        PageSize = pageSize
                    };
                   //0 0 1000
                    return View(resultd);
                }
                if (model.Min >= 0)
                {
                    var products = _productService.ListProduct()
                        .Where(x => x.Category.ParentCategoryId == model.CategoryId || x.CategoryId == model.CategoryId && x.Price >= model.Min)
                        .Skip(excludeRecords)
                        .Take(pageSize)
                        .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));
                    var resultd = new PagedResult<ListCategoryProductsModel>
                    {
                        Data = products.ToList(),
                        TotalItems = _productService
                            .ListProduct().Count(x => x.Category.ParentCategoryId == model.CategoryId || x.CategoryId == model.CategoryId && x.Price >= model.Min),
                        PageNumber = pageIndex,
                        PageSize = pageSize
                    };
                    //ok / main
                    return View(resultd);
                }

            }


            if (model.SearchCategoryId != 0 && model.Min >=0 || model.Max<= 0)
            {

                if (model.Min >= 0 && model.Max > 0)
                {
                    var products = _productService.ListProduct()
                        .Where(x => x.CategoryId == model.SearchCategoryId && x.Price >= model.Min && x.Price <= model.Max)
                        .Skip(excludeRecords)
                        .Take(pageSize)
                        .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));
                    var resultd = new PagedResult<ListCategoryProductsModel>
                    {
                        Data = products.ToList(),
                        TotalItems = _productService
                            .ListProduct().Count(x => x.CategoryId == model.SearchCategoryId && x.Price >= model.Min && x.Price <= model.Max),
                        PageNumber = pageIndex,
                        PageSize = pageSize
                    };
                  //android 0 1000
                    return View(resultd);
                }
                if (model.Min >= 0)
                {
                    var products = _productService.ListProduct()
                        .Where(x => x.CategoryId == model.SearchCategoryId && x.Price >= model.Min)
                        .Skip(excludeRecords)
                        .Take(pageSize)
                        .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));
                    var resultd = new PagedResult<ListCategoryProductsModel>
                    {
                        Data = products.ToList(),
                        TotalItems = _productService
                            .ListProduct().Count(x => x.CategoryId == model.SearchCategoryId && x.Price >= model.Min),
                        PageNumber = pageIndex,
                        PageSize = pageSize
                    };
                    //android 0 0
                    return View(resultd);
                }
            }
            return RedirectToAction("Index", "Home");
        }



        public IActionResult Search(ProductFilterViewModel model, string searchFor, int pageIndex = 1)
        {
            int pageSize = 6;
            int excludeRecords = (pageSize * pageIndex) - pageSize;

            ViewData["searchFor"] = searchFor;
            if (model.CategoryId == 0)
            {
                model.CategoryId = model.SearchCategoryId;
                ViewData["CategoryId"] = model.SearchCategoryId;
            }

            ViewData["CategoryId"] = model.CategoryId;
            if (model.SearchCategoryId == 0 && model.CategoryId != 0)
            {
                var getCategory = _categoryService.GetCategory(model.CategoryId);
                if (getCategory.ParentCategoryId != null)
                {
                    model.SearchCategoryId = model.CategoryId;
                }
            }

            ViewData["MaxPrice"] = model.Max;
            if (model.Min > model.Max && (decimal)model.Max == 0)
            {
                model.Max = _productService.ListProduct().Select(x => x.Price).Max();
            }
            ViewData["MinPrice"] = model.Min;
            ViewData["SearchCategoryName"] = model.SearchCategoryName;

            if (model.Min > model.Max)
            {
                ModelState.AddModelError("", "Minimum price cannot bigger than maximum price");
                var products = _productService.ListProduct()
                    .Where(x => x.ProductName.Contains(searchFor))
                    .Skip(excludeRecords)
                    .Take(pageSize)
                    
                    .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));

                var resultd = new PagedResult<ListCategoryProductsModel>
                {
                    Data = products.ToList(),
                    TotalItems = _productService.ListProduct().Count(x => x.ProductName.Contains(searchFor)),
                    PageNumber = pageIndex,
                    PageSize = pageSize
                };
                //min>max
                return View(resultd);
            }


            if (model.SearchCategoryId == 0)
            {
                if (model.Min >= 0 && model.Max > 0)
                {
                    var products = _productService.ListProduct()
                        .Where(x => x.ProductName.Contains(searchFor) && x.Price >= model.Min && x.Price <= model.Max)
                        .Skip(excludeRecords)
                        .Take(pageSize)
                        .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));
                    var resultd = new PagedResult<ListCategoryProductsModel>
                    {
                        Data = products.ToList(),
                        TotalItems = _productService
                            .ListProduct().Count(x => x.ProductName.Contains(searchFor) && x.Price >= model.Min && x.Price <= model.Max),
                        PageNumber = pageIndex,
                        PageSize = pageSize
                    };
                    //0 0 1000
                    return View(resultd);
                }
                if (model.Min >= 0)
                {
                    var products = _productService.ListProduct()
                        .Where(x => x.ProductName.Contains(searchFor) && x.Price >= model.Min)
                        .Skip(excludeRecords)
                        .Take(pageSize)
                        .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));
                    var resultd = new PagedResult<ListCategoryProductsModel>
                    {
                        Data = products.ToList(),
                        TotalItems = _productService
                            .ListProduct().Count(x => x.ProductName.Contains(searchFor) &&  x.Price >= model.Min),
                        PageNumber = pageIndex,
                        PageSize = pageSize
                    };
                    //main
                    return View(resultd);
                }

            }


            if (model.SearchCategoryId != 0 && model.Min >= 0 || model.Max <= 0)
            {

                if (model.Min >= 0 && model.Max > 0)
                {
                    var products = _productService.ListProduct()
                        .Where(x => x.ProductName.Contains(searchFor) && x.CategoryId == model.CategoryId && x.Price >= model.Min && x.Price <= model.Max)
                        .Skip(excludeRecords)
                        .Take(pageSize)
                        .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));
                    var resultd = new PagedResult<ListCategoryProductsModel>
                    {
                        Data = products.ToList(),
                        TotalItems = _productService
                            .ListProduct().Count(x => x.ProductName.Contains(searchFor) && x.CategoryId == model.CategoryId && x.Price >= model.Min && x.Price <= model.Max),
                        PageNumber = pageIndex,
                        PageSize = pageSize
                    };
                   //android 0 1000
                    return View(resultd);
                }
                if (model.Min >= 0)
                {
                    var products = _productService.ListProduct()
                        .Where(x => x.ProductName.Contains(searchFor) && x.CategoryId == model.CategoryId && x.Price >= model.Min)
                        .Skip(excludeRecords)
                        .Take(pageSize)
                        .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));
                    var resultd = new PagedResult<ListCategoryProductsModel>
                    {
                        Data = products.ToList(),
                        TotalItems = _productService
                            .ListProduct().Count(x =>x.ProductName.Contains(searchFor) && x.CategoryId == model.CategoryId && x.Price >= model.Min),
                        PageNumber = pageIndex,
                        PageSize = pageSize
                    };
                    //android 0 0
                    return View(resultd);
                }
            }
            return RedirectToAction("Index", "Home");


        }
    }
}
