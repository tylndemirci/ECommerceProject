﻿using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.DataAccess.Concrete
{
  public  class EfOrderDal : EfEntityRepository<Order>, IOrderDal
    {
        public EfOrderDal(ECommerceProjectContext dbContext): base(dbContext)
        {
            
        }
    }
}
