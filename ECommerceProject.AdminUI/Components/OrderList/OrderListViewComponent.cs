using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.AdminUI.Models.Order;
using ECommerceProject.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.AdminUI.Components.OrderList
{
    public class OrderListViewComponent: ViewComponent
    {
        private readonly IOrderService _orderService;
        private readonly IOrderLineService _orderLineService;

        public OrderListViewComponent(IOrderService orderService, IOrderLineService orderLineService)
        {
            _orderService = orderService;
            _orderLineService = orderLineService;
        }
        public IViewComponentResult Invoke()
        {
            var orders = _orderService.GetAllOrders().Include(x => x.OrderLines)
                .Select(x => new ListOrdersViewModel(x));
            return View(orders);
        }
    }
}
