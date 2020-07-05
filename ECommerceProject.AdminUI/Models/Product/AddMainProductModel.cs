using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceProject.AdminUI.Models.Product
{
    public class AddMainProductModel
    {
        public AddMainProductModel(Entities.Concrete.Product product)
        {
            
            CategoryId = product.CategoryId;
            Price = product.Price;
            ProductName = product.ProductName;
            ImageUrl = product.ImageUrl;
        }

       
        public AddMainProductModel()
        {
            
        }

        public AddMainProductModel(IQueryable<Entities.Concrete.Category> categories)
        {
            Categories = categories.Select(x => new SelectListItem(x.Title, x.Id.ToString(), false, false));
        }

        
        [Required] public int CategoryId { get; set; }
        [Required] public double Price { get; set; }
        [Required] public string ProductName { get; set; }
       
        public string ImageUrl { get; set; }
        public List<string> ProductDetailsTitle { get; set; }
        public List<string> ProductDetailsDescription { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}