using System.Collections.Generic;
using System.Linq;
using ECommerceProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceProject.AdminUI.Models.SearchBar
{
    public class SearchForUsersViewModel
    {
        public SearchForUsersViewModel(IQueryable<IdentityRole> roles)
        {
            Roles = roles.Select(x => new SelectListItem(x.Name, x.Id.ToString(), false, false));

        }

      
        
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
      
    }
}