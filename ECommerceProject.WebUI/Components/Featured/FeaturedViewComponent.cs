using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebUI.Components.Featured
{
    public class FeaturedViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public FeaturedViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            
            return View();
        }
    }
}
