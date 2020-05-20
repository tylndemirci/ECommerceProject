using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceProject.AdminUI.Models.Product
{
    public class AddProductViewModel
    {
        public AddProductViewModel(Entities.Concrete.Product product)
        {
            ProductId = product.ProductId;
            SubCategoryId = product.SubCategoryId;
            if (product.Category != null)
            {
                CategoryName = product.Category.Title;
                

            }
            Price = product.Price;
            ProductName = product.ProductName;
            ProductColor = product.ProductColor;
            ImageUrl = product.ImageUrl;
        }

        public AddProductViewModel()
        {

        }

        public AddProductViewModel(IQueryable<Entities.Concrete.Category> categories)
        {
            Categories = categories.Select(x=>new SelectListItem(x.Title, x.Id.ToString(),false,false));
             
        }


        public int ProductId { get; set; }
        
        public int SubCategoryId { get; set; }
       
        public string CategoryName { get; set; }
        public double Price { get; set; }
        public string ProductName { get; set; }
        public string ProductColor { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
