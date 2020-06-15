using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Entities.Concrete;
using ECommerceProject.WebUI.Models.ViewComponent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ECommerceProject.WebUI.Components.Explore
{
    public class ExploreViewComponent:ViewComponent
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ExploreViewComponent(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var allCategories = _categoryService.ListCategories().Select(x => x.Id);
            List<Product> products = new List<Product>();
            
            
            foreach (var category in allCategories)
            {
                
                products.Add(_productService.GetProductByCategoryId(category).FirstOrDefault());
            }    

            var returnModel = products.Select(x => new NewProductsViewModel(x)).ToList();
            return View(returnModel);
        }
    }
}