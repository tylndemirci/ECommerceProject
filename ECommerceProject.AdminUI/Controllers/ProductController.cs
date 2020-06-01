﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ECommerceProject.AdminUI.Models.Product;
using ECommerceProject.AdminUI.Models.Product.ProductDetails;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.AdminUI.Controllers
{
    public class ProductController : Controller
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

            var returnModel = new AddProductViewModel(_categoryService.GetAllWithSubNames());
            return View(returnModel);


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

                var product = new Product()
                {
                    ProductId = model.ProductId,
                    CategoryId = model.CategoryId,
                    Price = model.Price,
                    ProductName = model.ProductName,
                    ProductColor = model.ProductColor,
                    ImageUrl = model.ImageUrl ?? "productDefault.png"

                };

                _productService.AddProduct(product);

                product.ProductDetails = new List<ProductDetails>();

                //creates detailLine for each detail submitted.
                for (int i = 0; i < model.ProductDetailsTitle.Count; i++)
                {
                    var detailLine = new ProductDetails();
                    //detailLine.Id = model.ProductDetailsId;
                    detailLine.ProductId = product.ProductId;
                    detailLine.ProductDetailTitle = model.ProductDetailsTitle[i];
                        detailLine.ProductDetailDescription = model.ProductDetailsDescription[i];
                        
                    _productDetailsService.AddDetails(detailLine);
                }
                



                _productService.UpdateProduct(product);
                
                
               
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
            var getProduct = _productService.GetProduct(id);

            var returnModel = new UpdateProductViewModel(_categoryService.GetAllWithSubNames(), getProduct);


            return View(returnModel);
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

                var product = new Product
                {
                    ProductId = model.ProductId,
                    CategoryId = model.CategoryId,
                    Price = model.Price,
                    ProductName = model.ProductName,
                    Description = model.Description,
                    ProductColor = model.ProductColor,
                    ImageUrl = model.ImageUrl ?? "productDefault.png",
                   
                };


                _productService.UpdateProduct(product);

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult EditProductDetail(int productId)
        {
            var getDetails = _productDetailsService.GetAllDetails(productId).Where(x=>x.ProductId==productId && x.IsDeleted == false);
            var returnModel = new ProductDetailsModel();
            
            
            foreach (var detail in getDetails)
            {

                returnModel.ProductDetailIds.Add(detail.Id);
                returnModel.ProductDetailTitles.Add(detail.ProductDetailTitle);
                returnModel.ProductDetailDescriptions.Add(detail.ProductDetailDescription);

            }

            ViewBag.ProductId = productId;
            return View(returnModel);
        }

        [HttpPost]
        public IActionResult EditProductDetail(ProductDetailsModel model)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < model.ProductDetailTitles.Count; i++)
                {
                    var detail = _productDetailsService.GetDetail(model.ProductDetailIds[i]);
                    detail.ProductDetailTitle = model.ProductDetailTitles[i];
                    detail.ProductDetailDescription = model.ProductDetailDescriptions[i];
                    _productDetailsService.UpdateDetails(detail);
                }
                
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteProductDetail(int productDetailId)
        {
            var getDetail = _productDetailsService.GetDetail(productDetailId);
            var getProductId = getDetail.ProductId;
            _productDetailsService.DeleteDetails(getDetail.Id);

            return RedirectToAction("EditProductDetail", new {productId = getProductId});
        }

        //tekrar çağırınca bilgiler gelmiyor

        public IActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}