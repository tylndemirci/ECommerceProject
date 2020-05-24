﻿using System;
using System.Collections.Generic;
using System.Text;
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

            if (getOrder != null)
            {
                _orderDal.Update(getOrder);
            }

        }
    }
}