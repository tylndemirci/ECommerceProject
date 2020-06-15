using System.Linq;
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
