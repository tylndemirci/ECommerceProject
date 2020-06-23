using System.Linq;
using ECommerceProject.AdminUI.Models.SearchBar;
using ECommerceProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.AdminUI.Components.AdminUserSearchBar
{
    public class SearchForUsers:ViewComponent
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public SearchForUsers(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IViewComponentResult Invoke()
        {
            var returnModel = new SearchForUsersViewModel(_roleManager.Roles);
            return View(returnModel);
        }
    }
}