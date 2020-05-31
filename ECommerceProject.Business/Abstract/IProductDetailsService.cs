using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.Business.Abstract
{
  public  interface IProductDetailsService
  {
      IQueryable<ProductDetails> GetAllDetails(int productId);
      ProductDetails GetDetail(int detailId);
      void AddDetails(ProductDetails details);
      void UpdateDetails(ProductDetails details);
      void DeleteDetails(int detailsId);
  }
}
