using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Business.Abstract;
using ECommerceProject.WebUI.Models.ViewComponent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.WebUI.Components.NewProducts
{
    public class NewProductsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public NewProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }
            
        public IViewComponentResult Invoke()
        {
            
            var returnModel = _productService.ListProduct()
                .Include(x=> x.Category)
                .OrderByDescending(x=>x.ProductId)
                .Take(5)
                .Select(x=> new NewProductsViewModel(x)).ToList();
            

            return View(returnModel);
        }
    }
}
