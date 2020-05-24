﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.AdminUI.Models.Category;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Business.Concrete;
using ECommerceProject.WebUI.Models.ViewComponent;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebUI.Components.Navigation
{
    public class NavigationViewComponent: ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public NavigationViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        public IViewComponentResult Invoke(NavigationViewModel searchModel)
        {
            var returnModel = _categoryService.GetAllWithSubNames()
                .Where(f => f.IsDeleted == false)
                .Select(x => new NavigationViewModel(x));
            return View(returnModel);
        }
    }
}