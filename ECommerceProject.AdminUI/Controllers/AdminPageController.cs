using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Core.Enums;
using ECommerceProject.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using ECommerceProject.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.AdminUI.Controllers
{
    
    public class AdminPageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly IOrderLineService _orderLineService;

        public AdminPageController(UserManager<ApplicationUser> userManager, IOrderService orderService, IOrderLineService orderLineService)
        {
            _userManager = userManager;
            _orderService = orderService;
            _orderLineService = orderLineService;
        }

        public IActionResult Index()
        {
           var enumOrderStates = Enum.GetValues(typeof(EnumOrderState)).Cast<EnumOrderState>().Select(v =>
                new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList();
            List<ApplicationUser> list = new List<ApplicationUser>();
            foreach (var user in _userManager.Users) list.Add(user);
            ViewData["UserCount"] = list.Count;
            ViewData["NewUsers"] = _userManager.Users.Where(x=>x.CreationDate.Day<=30).ToList().Count;
            ViewData["TotalShop"] = _orderService.GetAllOrders().Where(x => x.OrderState == EnumOrderState.OrderCompleted).ToList().Count;
            ViewData["TotalOrders"] = _orderService.GetAllOrders().Count();
            ViewData["PendingOrders"] = _orderService.GetAllOrders().Count(x => x.OrderState != EnumOrderState.OrderCompleted);
            ViewData["ProductsSold"] = _orderService.GetAllOrders().Where(x=>x.OrderState == EnumOrderState.OrderCompleted).Select(x => x.OrderLines)
                .Select(x => x.Select(x => x.ProductId)).Count();
            return View();
        }

    }
}
