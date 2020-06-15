using System.Linq;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.Business.Abstract
{
    public interface ICategoryService
    {
        IQueryable<Category> GetAllWithSubNames();
        Category GetCategory(int id);
        IQueryable<Category> GetSubCategories(int categoryId);
        IQueryable<Category> GetAllSubCategories();
        Category GetSubCategoryForProduct(int productId);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
      

        void DeleteCategoryTree(int categoryId);
        
        
        
        IQueryable<Category> ListCategories();
     
    }
}
