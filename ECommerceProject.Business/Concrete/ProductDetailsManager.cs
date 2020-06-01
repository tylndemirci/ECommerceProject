using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECommerceProject.Business.Abstract;
using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.Business.Concrete
{
    public class ProductDetailsManager : IProductDetailsService
    {
        private readonly IProductDetailsDal _productDetailsDal;

        public ProductDetailsManager(IProductDetailsDal productDetailsDal)
        {
            _productDetailsDal = productDetailsDal;
        }

        public IQueryable<ProductDetails> GetAllDetails(int productId)
        {
            var getDetails = _productDetailsDal.GetAll(x => x.ProductId == productId);
            return getDetails;
        }

        public ProductDetails GetDetail(int detailId)
        {
            var getDetail = _productDetailsDal.GetBy(x => x.Id == detailId);
            return getDetail;
        }

        public void AddDetails(ProductDetails details)
        {
            var detail = new ProductDetails();
            detail.ProductId = details.ProductId;
            detail.ProductDetailTitle = details.ProductDetailTitle;
            detail.ProductDetailDescription = details.ProductDetailDescription;

            _productDetailsDal.Add(detail);

        }

        public void UpdateDetails(ProductDetails details)
        {
            var detail = _productDetailsDal.GetBy(x => x.Id == details.Id);

            detail.ProductId = details.ProductId;
            detail.ProductDetailTitle = details.ProductDetailTitle;
            detail.ProductDetailDescription = details.ProductDetailDescription;

            _productDetailsDal.Update(detail);

        }

        public void DeleteDetails(int detailsId)
        {
            var details = _productDetailsDal.GetBy(x => x.Id == detailsId);
            if (details != null)
            {
                details.IsDeleted = true;
                    _productDetailsDal.Update(details);
               
            }
        }
    }
}
