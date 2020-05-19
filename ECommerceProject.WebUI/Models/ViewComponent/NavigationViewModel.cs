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
        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public Category ParentCategory { get; set; }
        public string Title { get; set; }
    }
}

