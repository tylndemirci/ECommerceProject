using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ECommerceProject.WebUI.Models.Cart;

namespace ECommerceProject.WebUI.Models.Order
{
    public class OrderDetailsModel
    {

        public OrderDetailsModel(Entities.Concrete.Order model)
        {
            
            Name = model.Name;
            Surname = model.Surname;
            Address = model.Address;
            Country = model.Country;
            City = model.City;
            District = model.District;
            AddressTitle = model.AddressTitle;
            Phone = model.Phone;
            ProductName = new List<string>();
            Price = new List<double>();
            Quantity = new List<int>();
        }

        public OrderDetailsModel()
        {
            ProductName = new List<string>();
            Price = new List<double>();
            Quantity = new List<int>();
        }
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string AddressTitle { get; set; }
        [Required]
        public string Phone { get; set; }
        
        public List<string> ProductName { get; set; }
        public int TotalProductQuantity { get; set; }
        public List<int> Quantity { get; set; }
        public List<double> Price { get; set; }
        public double TotalPrice { get; set; }
        public string UserId { get; set; }
        

    }
}
