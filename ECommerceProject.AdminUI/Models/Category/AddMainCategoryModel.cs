using System.ComponentModel.DataAnnotations;

namespace ECommerceProject.AdminUI.Models.Category
{
    public class AddMainCategoryModel
    {
        public AddMainCategoryModel(Entities.Concrete.Category category)
        {
            Title = category.Title;
        }

        public AddMainCategoryModel()
        {
            
        }
        public int CategoryId { get; set; }
       [Required] public string Title { get; set; }
      
    }
}