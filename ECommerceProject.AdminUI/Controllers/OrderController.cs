using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ECommerceProject.AdminUI.Models.Order;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Core.Enums;
using ECommerceProject.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.AdminUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderLineService _orderLineService;

        public OrderController(IOrderService orderService, IOrderLineService orderLineService)
        {
            _orderService = orderService;
            _orderLineService = orderLineService;
        }

        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders().Include(x => x.OrderLines)
                .Select(x => new ListOrdersViewModel(x));
            return View(orders);


        }

       [HttpGet]
        public IActionResult Edit(int id)
        {
            var order = _orderService.GetOrder(id);
            var returnModel = new ListOrdersViewModel(order);
            return PartialView("_EditOrderPartialView", returnModel);
        }

        [HttpPost]
        public IActionResult Edit(ListOrdersViewModel model)
        {
            
            
            var order = new Order();
            order.OrderId = model.OrderId;
            order.OrderState = model.OrderState;
            _orderService.UpdateOrder(order);
         
            return PartialView("_EditOrderPartialView", model);
        }

        public IActionResult ShowOrderLines(int id)
        {

            var orderLines = _orderLineService.GetOrderLines(id).Include(x=>x.Product).Select(x => new ShowOrderLinesViewModel(x));

            return PartialView("_ShowOrderLines", orderLines);
        }

        public IActionResult OrderListInvoke()
        {
            return ViewComponent("OrderList");
        }
    }
}
