using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Entities.Concrete;

namespace ECommerceProject.AdminUI.Controllers
{
    public class AdminPageController : Controller
    {
        
        public IActionResult AdminInterface()
        {
            return View();
        }

    }
}
