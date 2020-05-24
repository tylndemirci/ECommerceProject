using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.WebUI.Models.Product
{
    public class ProductDetailsModel
    {
        public ProductDetailsModel(Entities.Concrete.Product product)
        {
            ProductId = product.ProductId;
            CategoryId = product.CategoryId;
            CategoryName = product.Category.Title;
            Count = product.Count;
            Price = product.Price;
            IsStock = product.Count >= 1;
            ProductName = product.ProductName;
            Description = product.Description;
            ProductColor = product.ProductColor;
            ImageUrl = product.ImageUrl;
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public bool IsStock { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductColor { get; set; }
        public string ImageUrl { get; set; }
    }
}
