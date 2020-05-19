using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ECommerceProject.AdminUI.Models.Product;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            var returnModel = _productService.ListProduct()
                .Where(x => x.IsDeleted != true)
                .Include(x => x.Category)
                .Select(x => new ListAllProductsViewModel(x));
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
            if (ModelState.IsValid)
            {


                if (file != null)
                {
                    if (file.FileName.EndsWith("jpg") || file.FileName.EndsWith("png"))
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//productimages", file.FileName);
                        await using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        model.ImageUrl = file.FileName;
                    }
                }



                _productService.AddProduct(new Product()
                {
                    ProductId = model.ProductId,
                    SubCategoryId = model.SubCategoryId,
                    Price = model.Price,
                    ProductName = model.ProductName,
                    ProductColor = model.ProductColor,
                    ImageUrl = model.ImageUrl ?? "productDefault.png"

                });
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            ViewBag.categories = _categoryService.ListCategories().Where(x => x.ParentCategoryId != null);
            var getProduct = _productService.GetProduct(id);
            var editProduct = new UpdateProductViewModel(getProduct);


            return View(editProduct);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(UpdateProductViewModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {


                if (file != null)
                {
                    if (file.FileName.EndsWith("jpg") || file.FileName.EndsWith("png"))
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//productimages", file.FileName);
                        await using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        model.ImageUrl = file.FileName;
                    }
                }


                _productService.UpdateProduct(new Product()
                {
                    ProductId = model.ProductId,
                    SubCategoryId = model.SubCategoryId,
                    Price = model.Price,
                    ProductName = model.ProductName,
                    Description = model.Description,
                    ProductColor = model.ProductColor,
                    ImageUrl = model.ImageUrl ?? "productDefault.png"
                });

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }


        public IActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}