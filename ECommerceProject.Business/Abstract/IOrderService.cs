using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.Business.Abstract
{
    public interface IOrderService
    {
        void UpdateOrder(Order order);
        IQueryable<Order> GetOrdersOfUser(string userName);
    }
}
