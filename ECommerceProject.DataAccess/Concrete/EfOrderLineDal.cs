using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.DataAccess.Concrete
{
    public class EfOrderLineDal : EfEntityRepository<OrderLine>, IOrderLineDal
    {
        public EfOrderLineDal(ECommerceProjectContext dbContext) : base(dbContext)
        {

        }
    }
}
