using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceProject.AdminUI.Models.Product
{
    public class AddProductViewModel
    {
        public AddProductViewModel(Entities.Concrete.Product product)
        {
            ProductId = product.ProductId;
            SubCategoryId = product.SubCategoryId;
          
            Price = product.Price;
            ProductName = product.ProductName;
            ProductColor = product.ProductColor;
            ImageUrl = product.ImageUrl;
        }

        public AddProductViewModel()
        {

        }


        public int ProductId { get; set; }
        public int SubCategoryId { get; set; }
       
        public double Price { get; set; }
        public string ProductName { get; set; }
        public string ProductColor { get; set; }
        public string ImageUrl { get; set; }
    }
}
