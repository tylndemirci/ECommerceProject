using System;
using System.Collections.Generic;
using System.Text;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.Business.Abstract
{
    public interface IOrderService
    {
        void UpdateOrder(Order order);
    }
}
