using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.AdminUI.Models.Order;
using ECommerceProject.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.AdminUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders().Include(x => x.OrderLines)
                .Select(x => new ListOrdersViewModel(x));
            return View(orders);
        }

       
        public IActionResult Edit(int id)
        {
         var order = _
           
           //var returnModel = new ListOrdersViewModel(order);
           return View(/*returnModel*/);
        }
    }
}
