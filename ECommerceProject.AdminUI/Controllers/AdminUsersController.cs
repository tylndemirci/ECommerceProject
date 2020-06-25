using System;
using System.Collections.Generic;
using System.Linq;
using cloudscribe.Pagination.Models;
using ECommerceProject.AdminUI.Models.AdminRole;
using ECommerceProject.AdminUI.Models.SearchBar;
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

        private static int pageSize = 10;

        [HttpGet]
        public IActionResult Search(int pageIndex = 1)
        {
            pageSize = 50;
            int excludeRecords = (pageSize * pageIndex) - pageSize;
            var usersReturning = _userManager.Users.Skip(excludeRecords).Take(pageSize);
            foreach (var user in usersReturning)
            {
                var role = _userManager.GetRolesAsync(user).Result;
                user.Role = role.FirstOrDefault();
            }

            var returningFor = usersReturning.Select(x =>
                new ViewUsersViewModel(x));
            var returningModel = new PagedResult<ViewUsersViewModel>
            {
                Data = returningFor.ToList(),
                TotalItems = returningFor.Count(),
                PageNumber = pageIndex,
                PageSize = pageSize
            };
            return View(returningModel);
        }

        [HttpPost]
        public IActionResult Search(SearchForUsersViewModel retModel, int pageIndex = 1)
        {
            int excludeRecords = (pageSize * pageIndex) - pageSize;
            if (retModel.UserName != null && retModel.RoleName == null)
            {
                var users = _userManager.Users.Where(x => x.UserName.Contains(retModel.UserName)).Skip(excludeRecords).Take(pageSize);
                foreach (var user in users)
                {
                    var role = _userManager.GetRolesAsync(user).Result;
                    user.Role = role.FirstOrDefault();
                }

                var returnFor = users.Select(x =>
                    new ViewUsersViewModel(x));
                var returnModel = new PagedResult<ViewUsersViewModel>
                {
                    Data = returnFor.ToList(),
                    TotalItems = returnFor.Count(),
                    PageNumber = pageIndex,
                    PageSize = pageSize
                };
                return View(returnModel);
            }


            if (retModel.UserName == null && retModel.RoleName != null)
            {
                var roleName = _roleManager.Roles.FirstOrDefault(x => x.Id == retModel.RoleName);
                var users = _userManager.GetUsersInRoleAsync(roleName.Name).Result.Skip(excludeRecords).Take(pageSize);
                foreach (var user in users)
                {
                    var role = _userManager.GetRolesAsync(user).Result;
                    user.Role = role.FirstOrDefault();
                }

                var returnFor = users.Select(x =>
                    new ViewUsersViewModel(x));
                var returnModel = new PagedResult<ViewUsersViewModel>
                {
                    Data = returnFor.ToList(),
                    TotalItems = returnFor.Count(),
                    PageNumber = pageIndex,
                    PageSize = pageSize
                };
                return View(returnModel);
            }

            if (retModel.UserName != null && retModel.RoleName != null)
            {
                var roleName = _roleManager.Roles.FirstOrDefault(x => x.Id == retModel.RoleName);
                var users = _userManager.GetUsersInRoleAsync(roleName.Name).Result.Where(x => x.UserName.Contains(retModel.UserName)).Skip(excludeRecords).Take(pageSize);
                foreach (var user in users)
                {
                    var role = _userManager.GetRolesAsync(user).Result;
                    user.Role = role.FirstOrDefault();
                }

                var returnFor = users.Select(x =>
                    new ViewUsersViewModel(x));
                var returnModel = new PagedResult<ViewUsersViewModel>
                {
                    Data = returnFor.ToList(),
                    TotalItems = returnFor.Count(),
                    PageNumber = pageIndex,
                    PageSize = pageSize
                };
                return View(returnModel);
            }

            var usersReturning = _userManager.Users.Skip(excludeRecords).Take(pageSize);
            foreach (var user in usersReturning)
            {
                var role = _userManager.GetRolesAsync(user).Result;
                user.Role = role.FirstOrDefault();
            }

            var returningFor = usersReturning.Select(x =>
                new ViewUsersViewModel(x));
            var returningModel = new PagedResult<ViewUsersViewModel>
            {
                Data = returningFor.ToList(),
                TotalItems = returningFor.Count(),
                PageNumber = pageIndex,
                PageSize = pageSize
            };
            return View(returningModel);
        }
    }
}