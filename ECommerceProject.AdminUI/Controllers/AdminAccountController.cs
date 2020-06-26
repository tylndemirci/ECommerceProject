using System.Threading.Tasks;
using ECommerceProject.AdminUI.Models.Identity;
using ECommerceProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.AdminUI.Controllers
{
  
    public class AdminAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AdminAccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }


        public IActionResult AdminLogin(string returnUrl)
        {

            ViewBag.returnUrl = returnUrl ?? "/" ;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(AdminLoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }

                ModelState.AddModelError("", "Email or password is invalid");
            }

            ViewBag.returnUrl = returnUrl ?? "/";
            return View(model);
        }
        

        public async Task<IActionResult> AdminLogout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}