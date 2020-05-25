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


       public Product GetProduct(int id)
       {
           var getProduct = _productDal.GetBy(x => x.ProductId == id);
           
           return getProduct;
       }

       public void AddProduct(Product product)
        {
           
            if (product!=null)
            {
                _productDal.Add(product);
            }
        }

        public void UpdateProduct(Product product, int productId)
        {
            var getProduct = _productDal.GetBy(x=>x.ProductId==product.ProductId);
            getProduct = product;
            getProduct.ProductId = productId;

            _productDal.Update(getProduct);

        }

        public void DeleteProduct(int productId)
        {
            var deleteProduct = _productDal.GetBy(p => p.ProductId == productId);
            if (deleteProduct!=null)
            {
                deleteProduct.IsDeleted = true;
                _productDal.Update(deleteProduct);
            }
            
        }

        public IQueryable<Product> ListProduct()
        {
            var returnProducts = _productDal.GetAll().Where(x=>x.IsDeleted==false);
            return returnProducts;
        }
   }
}
