using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceProject.WebUI.Models.ViewComponent.ProductFilter
{
    public class ProductFilterViewModel
    {
        public ProductFilterViewModel(IQueryable<Entities.Concrete.Category> categories)
        {
            Categories = categories.Select(x => new SelectListItem(x.Title, x.Id.ToString(), false, false));

        }

        public ProductFilterViewModel()
        {
                
        }

        public int CategoryId { get; set; }
        public int SearchCategoryId { get; set; }
        public string SearchCategoryName { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public int PageIndex { get; set; }

       






    }
}
