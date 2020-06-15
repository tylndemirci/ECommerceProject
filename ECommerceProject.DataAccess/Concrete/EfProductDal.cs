using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.DataAccess.Concrete
{
    public class EfProductDal : EfEntityRepository<Product>, IProductDal
    {
        public EfProductDal(ECommerceProjectContext dbContext) : base(dbContext)
        {
        }
    }
}
