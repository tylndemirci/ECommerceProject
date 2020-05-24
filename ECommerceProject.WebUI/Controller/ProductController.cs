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

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult ProductDetails(int productId)
        {
            var getProduct = _productService.GetProduct(productId);
            var setProduct = new ProductDetailsModel(getProduct);

            if (getProduct!=null)
            {
                return View(setProduct);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }
    }
}
