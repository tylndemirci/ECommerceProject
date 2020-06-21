using System.Linq;
using ECommerceProject.Business.Abstract;
using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.Business.Concrete
{
  public  class OrderManager:IOrderService
  {
      private readonly IOrderDal _orderDal;
  

      public OrderManager(IOrderDal orderDal)
      {
          _orderDal = orderDal;
      }

        public void UpdateOrder(Order order)
        {
            var getOrder = _orderDal.GetBy(x => x.OrderId == order.OrderId);
            getOrder.OrderId = order.OrderId;
            getOrder.OrderState = order.OrderState;
            

            if (getOrder != null)
            {
                _orderDal.Update(getOrder);
            }

        }
        
        public void CreateOrder(Order order)
        {
            var returnOrder= new Order();
            returnOrder.Address = order.Address;
            returnOrder.City = order.City;
            returnOrder.Country = order.Country;
            returnOrder.District = order.District;
            returnOrder.Name = order.Name;
            returnOrder.Phone = order.Phone;
            returnOrder.Surname = order.Surname;
            returnOrder.Total = order.Total;
            returnOrder.AddressTitle = order.AddressTitle;
            returnOrder.OrderDate = order.OrderDate;
            returnOrder.OrderLines = order.OrderLines;
            returnOrder.OrderNumber = order.OrderNumber;
            returnOrder.OrderState = order.OrderState;
            returnOrder.UserName = order.UserName;
            _orderDal.Add(order);

        }

        public IQueryable<Order> GetOrdersOfUser(string userName)
        {
            var orders = _orderDal.GetAll(x => x.UserName == userName);
            return orders;
        }

        public IQueryable<Order> GetAllOrders()
        {
            var allOrders = _orderDal.GetAll();
            return allOrders;
        }



        public Order GetOrder(int id)
        {
            var order = _orderDal.GetBy(x => x.OrderId == id);
            return order;
        }

        public IQueryable<OrderLine> GetOrderLines(int id)
        {
            var orderLines = _orderDal.GetBy(x => x.OrderId == id);
            var returnModel = orderLines.OrderLines.Where(x=>x.OrderId==id);
            return (IQueryable<OrderLine>)returnModel;
        }
    }
}
