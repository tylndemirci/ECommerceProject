using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using ECommerceProject.Business.Abstract;
using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }


        public void AddCategory(Category category)
        {
            _categoryDal.Add(category);
        }


        public void UpdateCategory(Category category)
        {
            _categoryDal.Update(category);
        }



        public void DeleteCategoryTree(int categoryId)
        {
            var deleting = _categoryDal.GetBy(p => p.Id == categoryId);
            



                if (deleting.SubCategories != null)
            {
                foreach (var subCategory in deleting.SubCategories)
                {
                    _categoryDal.Delete(subCategory);
                }
            }
            _categoryDal.Delete(deleting);

            
        }

       

   
        public IQueryable<Category> ListCategories()
        {
            return _categoryDal.GetAll()
                    .Include(x => x.SubCategories)
                    .Include(x => x.ParentCategory)
                ;
        }

       
    }
}
