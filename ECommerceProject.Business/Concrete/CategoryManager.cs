using System.Collections.Generic;
using System.Linq;
using ECommerceProject.Business.Abstract;
using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
            var subCategories = _categoryDal.GetAll().Where(x => x.ParentCategoryId.ToString() != null);
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

        
        public void DeleteCategoryTree(int categoryId)
        {
            var firstCat = _categoryDal.GetBy(x => x.Id == categoryId);
            var secCat = _categoryDal.GetAll().Where(x => x.ParentCategoryId == firstCat.Id);
            if (firstCat != null)
            {
                firstCat.IsDeleted = true;
                _categoryDal.UpdateWithoutSave(firstCat);
                var listCat = new List<Category>();

                for (int i = 0; i <= listCat.Count; i++)
                {
                    if (secCat.Any())
                    {
                        foreach (var category in secCat.ToList())
                        {
                            category.IsDeleted = true;
                            _categoryDal.UpdateWithoutSave(category);
                            listCat.Add(category);
                        }
                    }

                    if (i < listCat.Count)
                    {
                        firstCat = _categoryDal.GetBy(x => x.Id == listCat[i].Id);
                        secCat = _categoryDal.GetAll().Where(x => x.ParentCategoryId == firstCat.Id);
                    }
                }

                _categoryDal.Update(firstCat);
            }
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