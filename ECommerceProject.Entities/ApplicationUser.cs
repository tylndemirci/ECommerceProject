using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ECommerceProject.Entities
{
   public class ApplicationUser:IdentityUser
    {
        [Required]public string Name { get; set; }
        [Required] public string Surname { get; set; }

        public string Role { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }  
    }
}
