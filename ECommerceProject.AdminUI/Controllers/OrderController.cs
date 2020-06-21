using System.ComponentModel.DataAnnotations;
using System.Linq;
using ECommerceProject.AdminUI.Models.Order;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.AdminUI.Controllers
{
    [Authorize]
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
            //var orders = _orderService.GetAllOrders().Include(x => x.OrderLines)
            //    .Select(x => new ListOrdersViewModel(x));
            return View(/*orders*/);


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
