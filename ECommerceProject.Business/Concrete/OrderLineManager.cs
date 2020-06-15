using System.Linq;
using ECommerceProject.Business.Abstract;
using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.Business.Concrete
{
   public class OrderLineManager:IOrderLineService
   {
       private readonly IOrderLineDal _orderLineDal;

       public OrderLineManager(IOrderLineDal orderLineDal)
       {
           _orderLineDal = orderLineDal;
       }

       public IQueryable<OrderLine> GetOrderLines(int id)
       {
           var orderLine = _orderLineDal.GetAll().Where(x => x.OrderId == id);
           return orderLine;
       }
    }
}
