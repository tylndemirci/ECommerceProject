using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ECommerceProject.AdminUI.Models.AdminRole
{
    public class CreateAdminRoleModel
    { 

        
        public CreateAdminRoleModel()
        {
            
        }
        
       
      public string RoleName { get; set; }
    }
}