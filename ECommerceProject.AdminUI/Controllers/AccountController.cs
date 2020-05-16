using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.AdminUI.Models.Identity;
using ECommerceProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.AdminUI.Controllers
{
    [Route("/AdminAccount")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            IPasswordHasher<ApplicationUser> passwordHasher, IPasswordValidator<ApplicationUser> passwordValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
        }


        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
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

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.Email = model.Email;
                user.Name = model.Name;
                user.Surname = model.Surname;

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
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

        public async Task<IActionResult> UpdateUser(string id, string password, string email)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user!=null)
            {
                user.Email = email;

                IdentityResult validPass =null;

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