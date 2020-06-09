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
        private readonly IProductDal _productDal;

        public CategoryManager(ICategoryDal categoryDal, IProductDal productDal)
        {
            _categoryDal = categoryDal;
            _productDal = productDal;
        }

        public IQueryable<Category> GetAllWithSubNames()
        {
            return _categoryDal.GetAll().Where(x => x.IsDeleted == false)
                    .Include(x => x.SubCategories)
                    .Include(x => x.ParentCategory)
                ;
        }


        public Category GetCategory(int id)
        {
            var returnCategory = _categoryDal.GetBy(x => x.Id == id);
            return returnCategory;
        }

        public IQueryable<Category> GetSubCategories(int categoryId)
        {
            var subCategories = _categoryDal.GetAll().Where(x=>x.ParentCategoryId.ToString()!=null);
            var returnModel = subCategories.Where(x => x.ParentCategoryId == categoryId);
            return returnModel;
        }

        public IQueryable<Category> GetAllSubCategories()
        {
            var returnAll = _categoryDal.GetAll().Where(x => x.ParentCategoryId != null);
            return returnAll;
        }

        public Category GetSubCategoryForProduct(int productId)
        {
            var getProduct = _productDal.GetBy(x => x.ProductId == productId);
            var subCategory = _categoryDal.GetBy(x => x.Id == getProduct.CategoryId);
            return subCategory;
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
