using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceProject.WebUI.Models.Category
{
    public class ListCategoryProductsModel
    {
        public ListCategoryProductsModel(Entities.Concrete.Product product)
        {
            ProductId = product.ProductId;
            CategoryId = product.CategoryId;
            CategoryName = product.Category.Title;
            Price = product.Price;
            ProductName = product.ProductName;
            ImageUrl = product.ImageUrl;
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        
        public double Price { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
    }
}
