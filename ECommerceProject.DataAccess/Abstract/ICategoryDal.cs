using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECommerceProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
        IQueryable<Category> GetAllWithSubNames();
    }
}
