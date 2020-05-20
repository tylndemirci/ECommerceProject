using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Business.Concrete;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.WebUI.Models.ViewComponent
{
    public class NavigationViewModel
    {
        public NavigationViewModel(Category category)
        {
            Id = category.Id;
            Title = category.Title;
        }
        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public string Title { get; set; }
    }
}

