using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Core.Enums;
using Microsoft.AspNetCore.Identity;
using ECommerceProject.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ECommerceProject.AdminUI.Controllers
{
    
    public class AdminPageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;
        

        public AdminPageController(UserManager<ApplicationUser> userManager, IOrderService orderService)
        {
            _userManager = userManager;
            _orderService = orderService;
            
        }

        
        public IActionResult Index()
        {
            List<ApplicationUser> list = new List<ApplicationUser>();
            foreach (var user in _userManager.Users) list.Add(user);
            ViewData["UserCount"] = list.Count;
            ViewData["NewUsers"] = _userManager.Users.Where(x=>x.CreationDate.Day<=30).ToList().Count;
            ViewData["TotalShop"] = _orderService.GetAllOrders().Where(x => x.OrderState == EnumOrderState.OrderCompleted).ToList().Count;
            ViewData["TotalOrders"] = _orderService.GetAllOrders().Count();
            ViewData["PendingOrders"] = _orderService.GetAllOrders().Count(x => x.OrderState != EnumOrderState.OrderCompleted);
            ViewData["ProductsSold"] = _orderService.GetAllOrders().Where(x=>x.OrderState == EnumOrderState.OrderCompleted).Select(x => x.OrderLines)
                .Select(x => x.Select(line => line.ProductId)).Count();
            return View();
        }

    }
}
