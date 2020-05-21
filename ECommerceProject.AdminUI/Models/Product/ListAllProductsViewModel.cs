using System.Linq;
using ECommerceProject.AdminUI.Models.Category;

namespace ECommerceProject.AdminUI.Models.Product
{
    public class ListAllProductsViewModel
    {

        public ListAllProductsViewModel(Entities.Concrete.Product product)
        {
            ProductId = product.ProductId;
            CategoryId = product.CategoryId;
            CategoryName = product.Category.Title;
            Count = product.Count;
            Price = product.Price;
            IsStock = product.Count > 1 ? true : false;
            IsApproved = false;
            IsFeatured = false;
            ProductName = product.ProductName;
            Description = product.Description;
            ProductColor = product.ProductColor;
            ImageUrl = product.ImageUrl ?? "~/assets/images/productDefault.png";
            
        }

        public ListAllProductsViewModel()
        {

        }


        public int ProductId { get; set; }
        public int CategoryId { get; set; }
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
