using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ECommerceProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceProject.AdminUI.Models.AdminRole
{
    public class ViewUsersViewModel
    {
        public ViewUsersViewModel(ApplicationUser user, IQueryable<IdentityRole> roles)
        {
            UserId = user.Id;
            UserName = user.UserName;
            Roles = roles.Select(x => new SelectListItem(x.Name, x.Id.ToString(), false, false));
        }
        
        public ViewUsersViewModel(IQueryable<IdentityRole> roles)
        {
            Roles = roles.Select(x => new SelectListItem(x.Name, x.Id.ToString(), false, false));

        }

        public ViewUsersViewModel(ApplicationUser user)
        {
            UserId = user.Id;
            UserName = user.UserName;
           
        }

        public ViewUsersViewModel()
        {
            
        }
        
        
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
        
    }
}