namespace ECommerceProject.WebUI.Models.ViewComponent.ProductFilter
{
    public class ProductFilterViewModel
    {

        public ProductFilterViewModel(Entities.Concrete.Category category)
        {
            
            CategoryId = category.Id;
            CategoryName = category.Title;

        }

        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        
    }
}
