using Microsoft.AspNetCore.Identity;

namespace ECommerceProject.AdminUI.Models.AdminRole
{
    public class AdminRoleIndexModel
    {
        public AdminRoleIndexModel(IdentityRole role)
        {
            Id = role.Id;
            Name = role.Name;
        }

        public AdminRoleIndexModel()
        {
            
        }

        public string Id { get; set; }
        public string Name { get; set; }

  
    }
}