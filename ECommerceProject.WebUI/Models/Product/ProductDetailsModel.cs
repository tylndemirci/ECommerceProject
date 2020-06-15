using System.Collections.Generic;

namespace ECommerceProject.WebUI.Models.Product
{
    public class ProductDetailsModel
    {
        public ProductDetailsModel(Entities.Concrete.Product product,Entities.Concrete.Category  category)
        {
            ProductId = product.ProductId;
            CategoryId = product.CategoryId;
            
            
            SubCategoryId = category.Id;
            Count = product.Count;
            Price = product.Price;
            IsStock = product.Count >= 1;
            ProductName = product.ProductName;
            Description = product.Description;
            ProductColor = product.ProductColor;
            ImageUrl = product.ImageUrl;
        }

        public ProductDetailsModel()
        {
            
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int Count { get; set; }
        public int SubCategoryId { get; set; }
        public double Price { get; set; }
        public bool IsStock { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductColor { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public List<int> ProductDetailsId { get; set; }
        public List<string> ProductDetailsTitle { get; set; }
        public List<string> ProductDetailsDescription { get; set; }
    }
}
