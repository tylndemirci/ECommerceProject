using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.DataAccess.Abstract
{
  public  interface IOrderLineDal: IEntityRepository<OrderLine>
    {
    }
}
