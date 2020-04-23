using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.Entities.Concrete
{
    public class Product
    {
        public int ProductId { get; set; }
        public int SubCategoryId { get; set; }
        public Category Category { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public bool IsStock { get; set; }
        public bool IsApproved { get; set; }
        public bool IsFeatured { get; set; } 
        public bool IsDeleted { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductColor { get; set; }
        public string ImageUrl { get; set; }





    }
}
