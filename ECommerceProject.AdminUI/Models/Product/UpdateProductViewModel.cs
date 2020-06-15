using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceProject.AdminUI.Models.Product
{
    public class UpdateProductViewModel
    {
        public UpdateProductViewModel(Entities.Concrete.Product product)
        {
            ProductId = product.ProductId;
            CategoryId = product.CategoryId;
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

        public UpdateProductViewModel(IQueryable<Entities.Concrete.Category> categories, Entities.Concrete.Product product)
        {
            Categories = categories.Select(x => new SelectListItem(x.Title, x.Id.ToString(), false, false));
            ProductId = product.ProductId;
            CategoryId = product.CategoryId;
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


        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int ProductDetailsId { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public bool IsStock { get; set; }
        public bool IsApproved { get; set; }
        public bool IsFeatured { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductColor { get; set; }
        public string ImageUrl { get; set; }
        public string ProductDetailsTitle { get; set; }
        public string ProductDetailsDescription { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

    }
}