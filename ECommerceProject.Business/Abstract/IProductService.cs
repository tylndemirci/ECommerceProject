using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ECommerceProject.Entities;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.Business.Abstract
{
   public interface IProductService
   {
       Product GetProduct(int id);
       void AddProduct(Product product);
       void UpdateProduct(Product product);
       void DeleteProduct(int productId);
       IQueryable<Product> ListProduct();

   }
}
