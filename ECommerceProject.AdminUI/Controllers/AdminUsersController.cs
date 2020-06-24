using System.Collections.Generic;
using System.Linq;
using cloudscribe.Pagination.Models;
using ECommerceProject.AdminUI.Models.AdminRole;
using ECommerceProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ECommerceProject.AdminUI.Controllers
{
    public class AdminUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminUsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        private static int pageSize = 100;
        [HttpGet]
        public IActionResult Search(int pageIndex = 1)
        {
            int excludeRecords = (pageSize * pageIndex) - pageSize;
            var users = _userManager.Users.Include(x=>x.Role);
           var model = new ViewUsersViewModel();
           foreach (var user in users)
           {
               string listForRole = "No Role";
               var role =  _userManager.GetRolesAsync(user).Result;
                model.UserId = user.Id;
                model.UserName = user.Name;
                
                foreach (var rol in role)
                {
                    listForRole = rol;
                }

                model.RoleName = listForRole;


            }

           var returning = users.Skip(excludeRecords)
                .Take(pageSize).Select(x => model);
            var returnModel = new PagedResult<ViewUsersViewModel>
            {
                Data = returning.ToList(),
                TotalItems = returning.Count(),
                PageNumber = pageIndex,
                PageSize = pageSize
            };
            return View(returnModel);
        }
        
     [HttpPost]
        public IActionResult Search(string? userName, string? roleName,int pageIndex = 1)
        {
           
            int excludeRecords = (pageSize * pageIndex) - pageSize;



            if (userName!=null && roleName == null )    
            {
                var users = _userManager.Users.Where(x=>x.UserName.Contains(userName)).Skip(excludeRecords)
                    .Take(pageSize).Select(x => new ViewUsersViewModel(x));
                var returnModel = new PagedResult<ViewUsersViewModel>
                {
                    Data = users.ToList(),
                    TotalItems = users.Count(),
                    PageNumber = pageIndex,
                    PageSize = pageSize
                };
                
                return View(returnModel);
            }
            if (userName==null && roleName != null )    
            {
                var users =_userManager.GetUsersInRoleAsync(roleName).Result.Skip(excludeRecords)
                    .Take(pageSize).Select(x => new ViewUsersViewModel(x));
                var returnModel = new PagedResult<ViewUsersViewModel>
                {
                    Data = users.ToList(),
                    TotalItems = users.Count(),
                    PageNumber = pageIndex,
                    PageSize = pageSize
                };
                
                return View(returnModel);
            }
            
            if (userName!=null && roleName != null )    
            {
                var users =_userManager.GetUsersInRoleAsync(roleName).Result.Where(x=>x.UserName.Contains(userName)).Skip(excludeRecords)
                    .Take(pageSize).Select(x => new ViewUsersViewModel(x));
                
                var returnModel = new PagedResult<ViewUsersViewModel>
                {
                    Data = users.ToList(),
                    TotalItems = users.Count(),
                    PageNumber = pageIndex,
                    PageSize = pageSize
                };
                
                return View(returnModel);
            }

            var usersReturning = _userManager.Users.Include(x=>x.Role).Skip(excludeRecords)
                .Take(pageSize).Select(x => new ViewUsersViewModel(x));
            var returningModel = new PagedResult<ViewUsersViewModel>
            {
                Data = usersReturning.ToList(),
                TotalItems = usersReturning.Count(),
                PageNumber = pageIndex,
                PageSize = pageSize
            };
                
            return View(returningModel);

        }
    }
}