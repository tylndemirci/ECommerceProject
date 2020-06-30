using System;
using System.Collections.Generic;
using System.Security.Claims;
using ECommerceProject.Business.Abstract;
using ECommerceProject.Core.Enums;
using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.Entities.Concrete;
using ECommerceProject.Entities.DomainModels;
using ECommerceProject.WebUI.Helper;
using ECommerceProject.WebUI.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebUI.Controller
{
    public class CartController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartSessionHelper _cartSessionHelper;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IOrderLineDal _orderLineDal;
        
        

        public CartController(ICartService cartService, ICartSessionHelper cartSessionHelper, IProductService productService, IOrderService orderService, IOrderLineDal orderLineDal)
        {
            _cartService = cartService;
            _cartSessionHelper = cartSessionHelper;
            _productService = productService;
            _orderService = orderService;
            _orderLineDal = orderLineDal;
        }
        //todo add tempdata
        public IActionResult AddToCart(int productId)
        {
            
            Product product = _productService.GetProduct(productId);
            if (product!=null)
            {
                var cart = _cartSessionHelper.GetCart("cart");
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                cart.UserId = userId;

                _cartService.AddToCart(cart, product);
                //tempdata
                _cartSessionHelper.SetCart("cart", cart);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            
            Product product = _productService.GetProduct(productId);
            if (product!=null)
            {
                var cart = _cartSessionHelper.GetCart("cart");
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                cart.UserId = userId;

                _cartService.RemoveFromCart(cart, productId);
                //tempdata
                _cartSessionHelper.SetCart("cart", cart);
            }


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Checkout()
        {
            var cart = _cartSessionHelper.GetCart("cart");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            cart.UserId = userId;
            var returnModel = new OrderDetailsModel();
            
            foreach (var cartLine in cart.CartLines)
            {
                returnModel.ProductName.Add(cartLine.Product.ProductName);
                returnModel.Price.Add(cartLine.Product.Price);
                returnModel.Quantity.Add(cartLine.Quantity);
               
            }
            returnModel.TotalProductQuantity = cart.TotalProductQuantity();
            returnModel.TotalPrice = cart.TotalPrice();
            returnModel.UserId = userId;
            return View(returnModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Checkout(OrderDetailsModel model)
        {
            var cart = _cartSessionHelper.GetCart("cart");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            cart.UserId = userId;
            if (cart.CartLines==null)
            {
                ModelState.AddModelError("", "There is no product in your cart");
            }

            if (ModelState.IsValid)
            {
                SaveOrder(cart, model);
                _cartSessionHelper.Clear();
                return RedirectToAction("Completed");

            }
            foreach (var cartLine in cart.CartLines)
            {
                model.ProductName.Add(cartLine.Product.ProductName);
                model.Price.Add(cartLine.Product.Price);
                model.Quantity.Add(cartLine.Quantity);
               
            }
            model.TotalProductQuantity = cart.TotalProductQuantity();
            model.TotalPrice = cart.TotalPrice();
            return View(model);
        }

        private void SaveOrder(Cart cart, OrderDetailsModel model)
        {
            var order = new Order();
            order.OrderNumber = "A" + (new Random()).Next(111111, 999999).ToString();
            order.Total = cart.TotalPrice();
            order.OrderDate = DateTime.Now;
            order.OrderState = EnumOrderState.WaitingForApproval;
            order.UserName = User.Identity.Name;
            order.AddressTitle = model.AddressTitle;
            order.Address = model.Address;
            order.Name = model.Name;
            order.Surname = model.Surname;
            order.Country = model.Country;
            order.City = model.City;
            order.District = model.District;
            order.Phone = model.Phone;


            order.OrderLines = new List<OrderLine>();

            foreach (var product in cart.CartLines)
            {
                
                    var orderLine = new OrderLine();
                    orderLine.Quantity = product.Quantity;
                    orderLine.Price = product.Product.Price;
                    orderLine.ProductId = product.Product.ProductId;
                    order.OrderLines.Add(orderLine);
                    _orderLineDal.AddWithoutSave(orderLine);
            }

            _orderService.CreateOrder(order);

        }

        public IActionResult Completed()
        {
            return View();
        }







    }
}