using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Entities.Concrete;
using ECommerceProject.WebUI.Models.Category;
using ECommerceProject.WebUI.Models.Product;
using ECommerceProject.WebUI.Models.ViewComponent.ProductFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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

            var getDetails = _productDetailsService.GetAllDetails(productId).Where(x=> x.IsDeleted == false);
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

        [Route("/Products")]
        public IActionResult ListCategoryProducts(ProductFilterViewModel model, int categoryId, int pageIndex = 1)
        {
            ViewBag.CategoryId = categoryId;
            var minPrice = model.Min;
            var maxPrice = model.Max;
            var subCategoryId = model.SearchCategoryId;
            var getCategoryId = model.CategoryId;
            int pageSize = 5;
            int excludeRecords = (pageSize * pageIndex) - pageSize;
            



            if (minPrice > maxPrice)
            {
                ModelState.AddModelError("", "Minimum price cannot bigger than maximum price");
                var products = _productService.ListProduct()
                    .Where(x => x.CategoryId == categoryId || x.Category.ParentCategoryId == categoryId)
                    .Skip(excludeRecords)
                    .Take(pageSize)
                    .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));
                var resultd = new PagedResult<ListCategoryProductsModel>
                {
                    Data = products.ToList(),
                    TotalItems = products.Count(),
                    PageNumber = pageIndex,
                    PageSize = pageSize
                };

                return View(resultd);
            }

            if (subCategoryId == 0 && minPrice == 0 && maxPrice == 0)
            {
                var products = _productService.ListProduct()
                    .Where(x => x.CategoryId == categoryId || x.Category.ParentCategoryId == categoryId)
                    .Skip(excludeRecords)
                    .Take(pageSize)
                    .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));
                var resultd = new PagedResult<ListCategoryProductsModel>
                {
                    Data = products.ToList(),
                    TotalItems = _productService
                        .ListProduct().Count(x => x.CategoryId == categoryId || x.Category.ParentCategoryId == categoryId),
                    PageNumber = pageIndex,
                    PageSize = pageSize
                };

                return View(resultd);
            }


            if (subCategoryId != 0 || minPrice >= 0 || maxPrice == 0)
            {
                if (maxPrice == 0)
                {
                    var products = _productService.ListProduct()
                        .Where(x => x.CategoryId == categoryId
                                    || x.Category.ParentCategoryId == categoryId
                                    && x.CategoryId == subCategoryId
                                    && x.Price >= minPrice)
                        .Skip(excludeRecords)
                        .Take(pageSize)
                        .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));
                    var resultd = new PagedResult<ListCategoryProductsModel>
                    {
                        Data = products.ToList(),
                        TotalItems = _productService
                            .ListProduct().Count(x => x.CategoryId == categoryId
                                                      || x.Category.ParentCategoryId == categoryId
                                                      && x.CategoryId == subCategoryId
                                                      && x.Price >= minPrice),
                        PageNumber = pageIndex,
                        PageSize = pageSize
                    };

                    return View(resultd);


                }

                var getMaxPrice = _productService.ListProduct().Select(x => x.Price).Max();
                var returnProducts = _productService.ListProduct()
                    .Where(x => x.CategoryId == categoryId
                                || x.Category.ParentCategoryId == categoryId
                                && x.CategoryId == subCategoryId
                                && x.Price >= minPrice && x.Price <= getMaxPrice)
                    .Skip(excludeRecords)
                    .Take(pageSize)
                    .Include(x => x.Category).Select(x => new ListCategoryProductsModel(x));

                var result = new PagedResult<ListCategoryProductsModel>
                {
                    Data = returnProducts.ToList(),
                    TotalItems = _productService
                        .ListProduct().Count(x => x.CategoryId == categoryId
                                                  || x.Category.ParentCategoryId == categoryId
                                                  && x.CategoryId == subCategoryId
                                                  && x.Price >= minPrice && x.Price <= getMaxPrice),
                    PageNumber = pageIndex,
                    PageSize = pageSize
                };

                return View(result);


            }

            return RedirectToAction("Index", "Home");





        }


    }
}
