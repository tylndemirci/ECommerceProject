using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Entities;

namespace ECommerceProject.AdminUI.Models.Identity
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
