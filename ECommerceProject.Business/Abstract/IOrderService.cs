using System.Linq;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.Business.Abstract
{
    public interface IOrderService
    {
        void UpdateOrder(Order order);
        IQueryable<Order> GetOrdersOfUser(string userName);
        IQueryable<Order> GetAllOrders();

        IQueryable<OrderLine> GetOrderLines(int id);
        Order GetOrder(int id);
    }
}
