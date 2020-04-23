using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.AdminUI.Models.Product;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.AdminUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {

            var returnModel = _productService.ListProduct().Select(x => new ListAllProductsViewModel(x));
            return View(returnModel);
        }


        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.categories = _categoryService.ListCategories().Where(x => x.ParentCategoryId != null);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel model, IFormFile file)
        {
            if (file != null)
            {
                if (file.FileName.EndsWith("jpeg") || file.FileName.EndsWith("png"))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//productimages", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    model.ImageUrl = file.FileName;
                }
            }

            _productService.AddProduct(new Product()
            {
                ProductId = model.ProductId,
                ImageUrl = model.ImageUrl
            });
            return RedirectToAction("Index");
        }
    }
}