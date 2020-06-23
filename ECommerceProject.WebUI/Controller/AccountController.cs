﻿using System;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Entities;
using ECommerceProject.WebUI.Helper;
using ECommerceProject.WebUI.Models.Identity;
using ECommerceProject.WebUI.Models.MyAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
 using Microsoft.AspNetCore.Mvc.Filters;

 namespace ECommerceProject.WebUI.Controller
{
    [Authorize]
    [Route("/Account")]
    public class AccountController : Microsoft.AspNetCore.Mvc.Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly ICartSessionHelper _cartSessionHelper;
        private readonly IOrderService _orderService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IPasswordHasher<ApplicationUser> passwordHasher, IPasswordValidator<ApplicationUser> passwordValidator, ICartSessionHelper cartSessionHelper, IOrderService orderService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
            _cartSessionHelper = cartSessionHelper;
            _orderService = orderService;
        }
        

        [Route("/Account/Login")]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl)
        {
            ViewBag.returnUrl = returnUrl ?? "/" ;
            return View();
        }

        [HttpPost]
        [Route("/Account/Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model, string? returnUrl)
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

            ViewBag.returnUrl = returnUrl?? "/";
            return View(model);
        }
        [HttpGet]
        [Route("/Account/Register")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("/Account/Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.Email = model.Email;
                user.UserName = model.Email;
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.CreationDate = DateTime.Now;
                

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var currentUser = _userManager.Users.FirstOrDefault(u => u.UserName == user.UserName);

                    if (currentUser != null)
                    {
                        result = await _userManager.AddToRoleAsync(currentUser, "User");
                        
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                    
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

        [HttpGet]
        [Route("/ChangeMyPassword")]
        public async Task<IActionResult> ChangeMyPassword()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
            

                var returnModel = new ChangePasswordViewModel();
                returnModel.Id = user.Id;

                return View(returnModel);
            }
            else
            {
                return RedirectToAction("MyAccount", "Account");
            }
        }
        [HttpPost]
        [Route("/ChangeMyPassword")]
        public async Task<IActionResult> ChangeMyPassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(model.OldPassword))
                {
                    var validatePass = await _passwordValidator.ValidateAsync(_userManager, user, model.OldPassword);
                    if (validatePass.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.NewPasswordToCheck))
                        {
                            if (model.NewPassword == model.NewPasswordToCheck)
                            {
                                user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                                var result = await _userManager.UpdateAsync(user);
                                if (result.Succeeded)
                                {
                                    TempData["message"] = "Password successfully changed.";
                                    return RedirectToAction("MyAccount");
                                }
                                else
                                {
                                    foreach (var resultError in result.Errors)
                                    {
                                        ModelState.AddModelError("", resultError.Description);
                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "New password does not match with other.");
                            }

                        }
                        else
                        {
                            foreach (var identityError in validatePass.Errors)
                            {
                                ModelState.AddModelError("", identityError.Description);

                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Old password does not match with the current one.");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Your old password cannot be empty.");
                }

                
            }
            else
            {
                ModelState.AddModelError("", "User not found");
            }

            return View(model);
        }

        [HttpGet]
        [Route("/ChangeMyEmail")]
        public async Task<IActionResult> ChangeMyEmail()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                var returnModel = new ChangeEmailViewModel();
                returnModel.Id = user.Id;

                return View(returnModel);
            }
            else
            {
                return RedirectToAction("MyAccount", "Account");
            }
        }

        [HttpPost]
        [Route("/ChangeMyEmail")]
        public async Task<IActionResult> ChangeMyEmail(ChangeEmailViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Password))
                {
                    var validatePass = await _passwordValidator.ValidateAsync(_userManager, user, model.Password);
                    if (validatePass.Succeeded)
                    {
                        user.Email = model.Email;
                        var result = await _userManager.UpdateAsync(user);
                                if (result.Succeeded)
                                {
                                    TempData["message"] = "Email successfully changed.";
                                    return RedirectToAction("MyAccount");
                                }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Password does not match.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email and Password cannot be empty.");
                } 
            }
            else
            {
                ModelState.AddModelError("", "User not found.");
            }
            return View(model);
        }

        [Route("/MyOrders")]
        public IActionResult MyOrders()
        {
            var user = HttpContext.User;
            var orders = _orderService.GetOrdersOfUser(user.Identity.Name);
            var returnModel = new MyOrdersViewModel();
            foreach (var order in orders)
            {
                returnModel.OrderDate.Add(order.OrderDate.ToShortDateString());
                returnModel.OrderId.Add(order.OrderId);
                returnModel.OrderNumber.Add(order.OrderNumber);
                returnModel.OrderState.Add(order.OrderState.ToString());
                returnModel.ProductName.Add(order.OrderLines.Select(x=>x.Product.ProductName).ToString());
                returnModel.Quantity.Add(Convert.ToInt32(order.OrderLines.Select(x=>x.Quantity)));
                returnModel.Price.Add(Convert.ToDouble(order.OrderLines.Select(x=>x.Product.Price)));
                returnModel.Total.Add(Convert.ToDouble(order.OrderLines.Select(x => x.Price)) * Convert.ToDouble(order.OrderLines.Select(x => x.Quantity)));

            }

            return View(returnModel);
        }

        [Route("/MyAccount")]
        public IActionResult MyAccount(MyAccountInfoViewModel model)
        {
            var user = _userManager.GetUserAsync(HttpContext.User);
            model.Name = user.Result.Name;
            model.Surname = user.Result.Surname;
            model.Email = user.Result.Email;

            return View(model);
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


        // public IActionResult AccessDenied()
        // {
        //     return View();
        // }


    }
}
