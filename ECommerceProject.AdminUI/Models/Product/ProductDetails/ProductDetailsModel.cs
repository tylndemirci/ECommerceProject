﻿using System.Collections.Generic;

namespace ECommerceProject.AdminUI.Models.Product.ProductDetails
{
    public class ProductDetailsModel
    {

        public ProductDetailsModel()
        {
            
            ProductDetailIds = new List<int>();
            ProductDetailTitles = new List<string>();
            ProductDetailDescriptions = new List<string>();
        }

      


        public int ProductId { get; set; }
        public List<int> ProductDetailIds { get; set; }
        public List<string> ProductDetailTitles { get; set; }
        public List<string> ProductDetailDescriptions { get; set; }
        public List<int> AddProductDetailIds { get; set; }
        public List<string> AddProductDetailTitles { get; set; }
        public List<string> AddProductDetailDescriptions { get; set; }
    }
}
