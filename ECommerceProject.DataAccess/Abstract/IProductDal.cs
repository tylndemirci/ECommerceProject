using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Entities;

namespace ECommerceProject.DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
    }
}
