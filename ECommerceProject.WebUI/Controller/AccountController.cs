using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Entities;
using ECommerceProject.WebUI.Helper;
using ECommerceProject.WebUI.Models;
using ECommerceProject.WebUI.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebUI.Controller
{
    [Route("/Account")]
    public class AccountController : Microsoft.AspNetCore.Mvc.Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly ICartSessionHelper _cartSessionHelper;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IPasswordHasher<ApplicationUser> passwordHasher, IPasswordValidator<ApplicationUser> passwordValidator, ICartSessionHelper cartSessionHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
            _cartSessionHelper = cartSessionHelper;
        }

        [Route("/Account/Login")]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("/Account/Login")]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            _cartSessionHelper.GetCart("cart");
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

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
        [Route("/Account/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("/Account/Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.Email = model.Email;
                user.UserName = model.Email;
                user.Name = model.Name;
                user.Surname = model.Surname;

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var identityError in result.Errors)
                    {
                        ModelState.AddModelError("", identityError.Description);
                    }
                }
            }
            return View(model);
        }
        [Route("/Account/UpdateUser")]
        public async Task<IActionResult> UpdateUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Route("/Account/UpdateUser")]
        public async Task<IActionResult> UpdateUser(string id, string password, string email)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;

                IdentityResult validPass = null;

                if (!string.IsNullOrEmpty(password))
                {
                    validPass = await _passwordValidator.ValidateAsync(_userManager, user, password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, password);
                    }
                    else
                    {
                        foreach (var identityError in validPass.Errors)
                        {
                            ModelState.AddModelError("", identityError.Description);
                        }
                    }
                }

                if (validPass.Succeeded)
                {

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var identityError in result.Errors)
                        {
                            ModelState.AddModelError("", identityError.Description);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found");
            }

            return View(user);
        }

        //public async Task<IActionResult> VerifyEmail(string userId, string code)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);

        //    if (user == null) return BadRequest();

        //    var result = await _userManager.ConfirmEmailAsync(user, code);

        //    if (result.Succeeded)
        //    {
        //        return View();
        //    }

        //    return BadRequest();
        //}

        //public IActionResult EmailVerification() => View();

        [Route("/Account/Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            
            
          
            
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
