using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.DataAccess.Concrete
{
    public class EfCategoryDal : EfEntityRepository<Category>, ICategoryDal
    {
        public EfCategoryDal(ECommerceProjectContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Category> GetAllWithSubNames()
        {
            return GetAll().Where(x=>x.IsDeleted==false)
                    .Include(x => x.SubCategories)
                    .Include(x => x.ParentCategory)
                ;
        }
    }
}
    