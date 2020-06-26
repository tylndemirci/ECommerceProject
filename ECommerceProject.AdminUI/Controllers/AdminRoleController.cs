using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.AdminUI.Models.AdminRole;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.AdminUI.Controllers
{
    public class AdminRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminRoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
           
        }

        public IActionResult Index()
        {
            var returnModel = _roleManager.Roles.Select(x => new AdminRoleIndexModel(x));
            return View(returnModel);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateAdminRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(model.RoleName));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var resultError in result.Errors)
                    {
                        ModelState.AddModelError("", resultError.Description);
                    }
                }
            }
            var errorList = new List<string>();
            if (ModelState.ErrorCount>0)
            {
                foreach (var error in ModelState.Values.Select(x=>x.Errors))
                {
                   errorList.Add(error.ToString());
                }
            }

            return View(model);
        }
    }
}