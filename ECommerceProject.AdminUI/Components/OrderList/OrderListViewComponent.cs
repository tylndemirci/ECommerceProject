using System.Linq;
using ECommerceProject.AdminUI.Models.Order;
using ECommerceProject.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.AdminUI.Components.OrderList
{
    public class OrderListViewComponent: ViewComponent
    {
        private readonly IOrderService _orderService;

        public OrderListViewComponent(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IViewComponentResult Invoke()
        {
            var orders = _orderService.GetAllOrders().Include(x => x.OrderLines)
                .Select(x => new ListOrdersViewModel(x));
            return View(orders);
        }
    }
}
