using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Entities.Concrete;
using ECommerceProject.WebUI.Models.Product;
using ECommerceProject.WebUI.Models.ViewComponent.ProductFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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


    }
}
