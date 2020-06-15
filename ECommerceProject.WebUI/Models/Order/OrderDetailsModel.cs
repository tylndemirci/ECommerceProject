namespace ECommerceProject.WebUI.Models.Order
{
    public class OrderDetailsModel
    {

        //public OrderDetailsModel(Entities.Concrete.Order model)
        //{
        //    UserName = UserName;
        //    Name = model.Name;
        //    Surname = model.Surname;
        //    Address = model.Address;
        //    Country = model.Country;
        //    City = model.City;
        //    District = model.District;
        //    AddressTitle = model.AddressTitle;
        //    Phone = model.Phone;

        //}

        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string AddressTitle { get; set; }
        public string Phone { get; set; }
        

    }
}
