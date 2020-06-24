using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.AdminUI.Infrastructure
{
    [HtmlTargetElement("td", Attributes = "user-role")]
    public class AdminPanelUserRoleHelper : TagHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminPanelUserRoleHelper(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HtmlAttributeName("user-role")] private string User { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var names = new List<string>();
            var comingUser = await _userManager.FindByIdAsync(User);
            if (comingUser != null)
            {
                var role = await _userManager.GetRolesAsync(comingUser);
                if (role != null)
                {
                    names.AddRange(role);
                }
            }

            output.Content.SetContent(names.Count == 0 ? "Has No Role" : string.Join(", ", names));
        }
    }
}