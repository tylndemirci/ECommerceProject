using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ECommerceProject.Entities;

namespace ECommerceProject.Business.Abstract
{
   public interface IProductService
   {
       void AddProduct(Product product);
       void UpdateProduct(Product product);
       void DeleteProduct(int productId);
            
   }
}
