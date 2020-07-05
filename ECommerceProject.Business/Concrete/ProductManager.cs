using System.Linq;
using ECommerceProject.Business.Abstract;
using ECommerceProject.DataAccess.Abstract;
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

       public IQueryable<Product> GetProductByCategoryId(int categoryId)
       {
           var getProduct = _productDal.GetAll(x => x.CategoryId == categoryId).Include(x=>x.Category);
           
           
           return getProduct;
       }

       public void AddProduct(Product product)
        {
           
            if (product!=null)
            {
                _productDal.Add(new Product
                {
                    ProductName = product.ProductName,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    CategoryId = product.CategoryId
                });
            }
        }

       public Product AddProductReturn(Product product)
       {
           var returnProduct = new Product
               {
                   ProductName = product.ProductName,
                   Price = product.Price,
                   ImageUrl = product.ImageUrl,
                   CategoryId = product.CategoryId
               };
               _productDal.Add(returnProduct);
               return returnProduct;
          

       }

       public void UpdateProduct(Product product)
        {
            var getProduct = _productDal.GetBy(x=>x.ProductId==product.ProductId);
            
            getProduct.CategoryId = product.CategoryId;
            getProduct.Count = product.Count;
            getProduct.Price = product.Price;
            getProduct.IsStock = product.IsStock;
            getProduct.IsApproved = product.IsApproved;
            getProduct.IsFeatured = product.IsFeatured;
            getProduct.ProductName = product.ProductName;
            getProduct.Description = product.Description;
            getProduct.ProductColor = product.ProductColor;
            getProduct.ImageUrl = product.ImageUrl ?? "~/assets/images/productDefault.png";


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
            var returnProducts = _productDal.GetAll().Where(x=>x.IsDeleted==false).Include(x=>x.Category);
            return returnProducts;
        }
   }
}
