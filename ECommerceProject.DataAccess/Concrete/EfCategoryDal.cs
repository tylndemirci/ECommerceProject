using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.DataAccess.Concrete
{
    public class EfCategoryDal : EfEntityRepository<Category>, ICategoryDal
    {
        public EfCategoryDal(ECommerceProjectContext dbContext) : base(dbContext)
        {
        }

       
    }
}
    