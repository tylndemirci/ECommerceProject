using System.Collections.Generic;
using System.Dynamic;
using ECommerceProject.Entities.DomainModels;

namespace ECommerceProject.WebUI.Models.Cart
{
    public class CartModel
    {
        public CartModel(string userId)
        {
            UserId = userId;
            ImageUrl = new List<string>();
            ProductName = new List<string>();
            Price = new List<double>();
            Quantity = new List<int>();
        }
        public List<string> ImageUrl { get; set; }
        public List<string> ProductName { get; set; }
        public List<double> Price { get; set; }
        public int TotalProductQuantity { get; set; }
        public List<int> Quantity { get; set; }
        public double TotalPrice { get; set; }
        
        public string UserId { get; set; }

       


    }
}