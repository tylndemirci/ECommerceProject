namespace ECommerceProject.WebUI.Models.ViewComponent.Footer
{
    public class FooterViewModel
    {
        public FooterViewModel(Entities.Concrete.Category category)
        {
            CategoryId = category.Id;
            CategoryName = category.Title;
            
        }


        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}