namespace ECommerceProject.WebUI.Models.ViewComponent
{
    public class NewProductsViewModel
    {
        public NewProductsViewModel(Entities.Concrete.Product product)
        {
            ProductId = product.ProductId;
            CategoryId = product.CategoryId;
            CategoryName = product.Category.Title;
            Price = product.Price;
            ProductName = product.ProductName;
            ImageUrl = product.ImageUrl;
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }

    }
}
