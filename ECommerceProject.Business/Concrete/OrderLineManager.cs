using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ECommerceProject.Business.Abstract;
using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

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
