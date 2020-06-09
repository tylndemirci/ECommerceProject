using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Business.Concrete;
using ECommerceProject.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceProject.WebUI.Models.ViewComponent
{
    public class NavigationViewModel
    {
        public NavigationViewModel(Entities.Concrete.Category category)
        {
            Id = category.Id;
            Title = category.Title;
            if (category.ParentCategoryId!=null)
            {
                IsSubCategory = true;
            }
        }

        public NavigationViewModel()
        {
            
        }

      
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsSubCategory { get; set; }
        public string ProductName { get; set; }

    }
}

