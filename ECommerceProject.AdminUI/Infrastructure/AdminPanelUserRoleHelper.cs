using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ECommerceProject.AdminUI.Infrastructure
{
    [HtmlTargetElement("td", Attributes = "user-role")]
    public class AdminPanelUserRoleHelper : TagHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminPanelUserRoleHelper(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HtmlAttributeName("user-role")] private static string User => null;

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