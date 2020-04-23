using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Business.Constants;
using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.Entities;
using ECommerceProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.Business.Concrete
{
   public class ProductManager:IProductService
   {
       private readonly IProductDal _productDal;

       public ProductManager(IProductDal productDal)
       {
           _productDal = productDal;
       }


       public void AddProduct(Product product)
        {
            _productDal.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            _productDal.Update(product);
        }

        public void DeleteProduct(int productId)
        {
            var deleteProduct = _productDal.GetBy(p => p.ProductId == productId);
            deleteProduct.IsDeleted = true;
            _productDal.Update(deleteProduct);
        }

        public IQueryable<Product> ListProduct()
        {
            var returnProducts = _productDal.GetAll();
            return returnProducts;
        }
   }
}
