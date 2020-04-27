using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.Business.Abstract
{
    public interface ICategoryService
    {
        Category GetCategory(int id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
      

        void DeleteCategoryTree(int categoryId);
        
        
        
        IQueryable<Category> ListCategories();
     
    }
}
