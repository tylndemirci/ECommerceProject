using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using ECommerceProject.Business.Abstract;
using ECommerceProject.DataAccess;
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


        public Category GetCategory(int id)
        {
            var returnCategory = _categoryDal.GetBy(x => x.Id == id);
            return returnCategory;
        }

        public void AddCategory(Category category)
        {
            _categoryDal.Add(category);
        }


        public void UpdateCategory(Category category)
        {
            _categoryDal.Update(category);
        }


        //todo: make it generic
        public void DeleteCategoryTree(int categoryId)
        {
            var deleting = _categoryDal.GetBy(p => p.Id == categoryId);
            var deletingSubs = _categoryDal.GetAll().Where(p => p.ParentCategoryId == deleting.Id);
            if (deletingSubs.Any())
            {
                foreach (var deletingSub in deletingSubs)
                {
                    deletingSub.IsDeleted = true;
                    _categoryDal.UpdateWithoutSave(deletingSub);
                    var deletingSubsSubs = _categoryDal.GetAll().Where(s => s.ParentCategoryId == deletingSub.Id);
                    if (deletingSubsSubs.Any())
                    {
                        foreach (var deletingSubsSub in deletingSubsSubs)
                        {
                            deletingSubsSub.IsDeleted = true;
                            _categoryDal.UpdateWithoutSave(deletingSubsSub);
                            var deletingSubsSubsSubs =
                                _categoryDal.GetAll().Where(a => a.ParentCategoryId == deletingSubsSub.Id);
                            if (deletingSubsSubsSubs.Any())
                            {
                                foreach (var deletingSubsSubsSub in deletingSubsSubsSubs)
                                {
                                    deletingSubsSubsSub.IsDeleted = true;
                                    _categoryDal.UpdateWithoutSave(deletingSubsSubsSub);
                                    var deletingSubsSubsSubsSubs = _categoryDal.GetAll()
                                        .Where(k => k.ParentCategoryId == deletingSubsSubsSub.Id);
                                    if (deletingSubsSubsSubsSubs.Any())
                                    {
                                        foreach (var deletingSubsSubsSubsSub in deletingSubsSubsSubsSubs)
                                        {
                                            deletingSubsSubsSubsSub.IsDeleted = true;
                                            _categoryDal.UpdateWithoutSave(deletingSubsSubsSubsSub);
                                        }
                                    }

                                }
                            }
                        }
                    }

                }

                


            }

           

            deleting.IsDeleted = true;
           
            _categoryDal.Update(deleting);

          
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
