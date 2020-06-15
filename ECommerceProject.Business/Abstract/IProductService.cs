using System.Linq;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.Business.Abstract
{
   public interface IProductService
   {
       Product GetProduct(int id);

       IQueryable<Product> GetProductByCategoryId(int categoryId);
       void AddProduct(Product product);
       void UpdateProduct(Product product);
       void DeleteProduct(int productId);
       IQueryable<Product> ListProduct();

   }
}
