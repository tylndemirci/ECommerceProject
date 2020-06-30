using System.Linq;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.Business.Abstract
{
  public  interface IOrderLineService
  {
      IQueryable<OrderLine> GetOrderLines(int id);
   
  }
}
