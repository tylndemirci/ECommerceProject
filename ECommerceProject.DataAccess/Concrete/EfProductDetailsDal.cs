using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.DataAccess.Concrete
{
   public class EfProductDetailsDal : EfEntityRepository<ProductDetails>, IProductDetailsDal
    {
        public EfProductDetailsDal(ECommerceProjectContext dbContext) : base(dbContext)
        {
        }
    }
}
