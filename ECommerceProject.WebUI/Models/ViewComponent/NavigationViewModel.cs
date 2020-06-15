namespace ECommerceProject.WebUI.Models.ViewComponent
{
    public class NavigationViewModel
    {
        public NavigationViewModel(Entities.Concrete.Category category)
        {
            Id = category.Id;
            Title = category.Title;
            if (category.ParentCategoryId!=null)
            {
                IsSubCategory = true;
            }
        }

        public NavigationViewModel()
        {
            
        }

      
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsSubCategory { get; set; }
        public string ProductName { get; set; }

    }
}

