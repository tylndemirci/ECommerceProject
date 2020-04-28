using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceProject.AdminUI.Models.Product
{
    public class UpdateProductViewModel
    {
        public UpdateProductViewModel(Entities.Concrete.Product product)
        {
            ProductId = product.ProductId;
            SubCategoryId = product.SubCategoryId;
            if (product.Category != null)
            {
                CategoryName = product.Category.Title;
            }
            Count = product.Count;
            Price = product.Price;
            IsStock = product.IsStock;
            IsApproved = product.IsApproved;
            IsFeatured = product.IsFeatured;
            
            ProductName = product.ProductName;
            Description = product.Description;
            ProductColor = product.ProductColor;
            ImageUrl = product.ImageUrl ?? "~/assets/images/productDefault.png";
        }

        public UpdateProductViewModel()
        {

        }

        public int ProductId { get; set; }
        public int SubCategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public bool IsStock { get; set; }
        public bool IsApproved { get; set; }
        public bool IsFeatured { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductColor { get; set; }
        public string ImageUrl { get; set; }
    }
}
