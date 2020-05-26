using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Business.Abstract;
using ECommerceProject.WebUI.Models.Category;
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
        public IActionResult ListCategoryProducts(int categoryId)
        {
            var products = _productService.ListProduct()
                .Where(x => x.CategoryId == categoryId)
                .Include(x => x.Category).Select(x=> new ListCategoryProductsModel(x));
            ViewBag.CategoryId = categoryId;
           
            return View(products);
        }
    }
}
