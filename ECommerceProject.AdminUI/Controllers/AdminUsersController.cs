using System.Collections.Generic;
using System.Linq;
using cloudscribe.Pagination.Models;
using ECommerceProject.AdminUI.Models.AdminRole;
using ECommerceProject.AdminUI.Models.SearchBar;
using ECommerceProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;

namespace ECommerceProject.AdminUI.Controllers
{
    public class AdminUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMemoryCache _memoryCache;

        public AdminUsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMemoryCache memoryCache)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _memoryCache = memoryCache;
        }

        // [HttpGet]
        // public IActionResult Search(int pageIndex = 1)
        // {
        //    int pageSize = 2;
        //     int excludeRecords = (pageSize * pageIndex) - pageSize;
        //     List<ApplicationUser> usersReturning;
        //     if (!_memoryCache.TryGetValue("UsersWithRole", out usersReturning))
        //     {
        //         _memoryCache.Set("UsersWithRole", _userManager.Users.ToList());
        //     }
        //     usersReturning = _memoryCache.Get("UsersWithRole") as List<ApplicationUser>;
        //     foreach (var user in usersReturning)
        //     {
        //         var role = _userManager.GetRolesAsync(user).Result;
        //         user.Role = role.FirstOrDefault();
        //     }
        //
        //     var returningFor = usersReturning.Skip(excludeRecords).Take(pageSize).Select(x =>
        //         new ViewUsersViewModel(x));
        //     var returningModel = new PagedResult<ViewUsersViewModel>
        //     {
        //         Data = returningFor.ToList(),
        //         TotalItems = _userManager.Users.Count(),
        //         PageNumber = pageIndex,
        //         PageSize = pageSize
        //     };
        //     return View(returningModel);
        // }
        //
        // [HttpPost]
        public IActionResult Search(SearchForUsersViewModel retModel, int pageIndex = 1)
        {
            var roleName = _roleManager.Roles.FirstOrDefault(x => x.Id == retModel.RoleName);
            ViewData["searchFor"] = retModel.UserName;
            
            ViewData["RoleName"] = retModel.RoleName;
            var pageSize = 1;
            int excludeRecords = (pageSize * pageIndex) - pageSize;
            if (retModel.UserName != null && retModel.RoleName == null)
            {
                List<ApplicationUser> users; 
                if (!_memoryCache.TryGetValue("UsersWithRole", out users))
                {
                    _memoryCache.Set("UsersWithRole", _userManager.Users.Where(x => x.UserName.Contains(retModel.UserName)).ToList());
                }
                users = _memoryCache.Get("UsersWithRole") as List<ApplicationUser>;
                foreach (var user in users)
                {
                    var role = _userManager.GetRolesAsync(user).Result;
                    user.Role = role.FirstOrDefault();
                }
               
                var returnFor = users.Skip(excludeRecords).Take(pageSize).Select(x =>
                    new ViewUsersViewModel(x));
                var returnModel = new PagedResult<ViewUsersViewModel>
                {
                    Data = returnFor.ToList(),
                    TotalItems = _userManager.Users.Count(x => x.UserName.Contains(retModel.UserName)),
                    PageNumber = pageIndex,
                    PageSize = pageSize
                };
                return View(returnModel);
            }


            if (retModel.UserName == null && retModel.RoleName != null)
            {
                
                List<ApplicationUser> users; 
                if (!_memoryCache.TryGetValue("UsersWithRole", out users))
                {
                    _memoryCache.Set("UsersWithRole", _userManager.GetUsersInRoleAsync(roleName.Name).Result.ToList());
                }
                users = _memoryCache.Get("UsersWithRole") as List<ApplicationUser>;
                foreach (var user in users)
                {
                    var role = _userManager.GetRolesAsync(user).Result;
                    user.Role = role.FirstOrDefault();
                }

                var returnFor = users.Skip(excludeRecords).Take(pageSize).Select(x =>
                    new ViewUsersViewModel(x));
                var returnModel = new PagedResult<ViewUsersViewModel>
                {
                    Data = returnFor.ToList(),
                    TotalItems = _userManager.GetUsersInRoleAsync(roleName.Name).Result.Count(),
                    PageNumber = pageIndex,
                    PageSize = pageSize
                };
                return View(returnModel);
            }

            if (retModel.UserName != null && retModel.RoleName != null)
            {
                
                List<ApplicationUser> users; 
                if (!_memoryCache.TryGetValue("UsersWithRole", out users))
                {
                    _memoryCache.Set("UsersWithRole", _userManager.GetUsersInRoleAsync(roleName.Name).Result.Where(x => x.UserName.Contains(retModel.UserName)).ToList());
                }
                users = _memoryCache.Get("UsersWithRole") as List<ApplicationUser>;
                foreach (var user in users)
                {
                    var role = _userManager.GetRolesAsync(user).Result;
                    user.Role = role.FirstOrDefault();
                }

                var returnFor = users.Skip(excludeRecords).Take(pageSize).Select(x =>
                    new ViewUsersViewModel(x));
                var returnModel = new PagedResult<ViewUsersViewModel>
                {
                    Data = returnFor.ToList(),
                    TotalItems = _userManager.GetUsersInRoleAsync(roleName.Name).Result.Count(x => x.UserName.Contains(retModel.UserName)),
                    PageNumber = pageIndex,
                    PageSize = pageSize
                };
                return View(returnModel);
            }
            pageSize = 20;
            List<ApplicationUser> usersReturning;
            if (!_memoryCache.TryGetValue("UsersWithRole", out usersReturning))
            {
                _memoryCache.Set("UsersWithRole", _userManager.Users.ToList());
            }
            usersReturning = _memoryCache.Get("UsersWithRole") as List<ApplicationUser>;
            foreach (var user in usersReturning)
            {
                var role = _userManager.GetRolesAsync(user).Result;
                user.Role = role.FirstOrDefault();
            }
            
            var returningFor = usersReturning.Skip(excludeRecords).Take(pageSize).Select(x =>
                new ViewUsersViewModel(x));
            var returningModel = new PagedResult<ViewUsersViewModel>
            {
                Data = returningFor.ToList(),
                TotalItems = _userManager.Users.Count(),
                PageNumber = pageIndex,
                PageSize = pageSize
            };
            return View(returningModel);
        }

        [HttpGet]
        public IActionResult ChangeRole()
        {
            return View();
        }
    }
}