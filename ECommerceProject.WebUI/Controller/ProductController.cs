using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Business.Abstract;
using ECommerceProject.WebUI.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebUI.Controller
{
    public class ProductController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [Route("/ProductDetails")]
        public IActionResult ProductDetails(int productId)
        {
            
                var getProduct = _productService.GetProduct(productId);
                var setProduct = new ProductDetailsModel(getProduct);
                //var category = _categoryService.GetCategory(getProduct.CategoryId);

                return View(setProduct);
           //todo return to home in case something occurs.

                
            
           
        }
    }
}
