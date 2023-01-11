using Domain.EntityFramework;
using Domain.Model.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Cart;
using ShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ShoppingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICartProductRepository _cartProductRepository;

        public HomeController(ILogger<HomeController> logger, ICartProductRepository cartProductRepository)
        {
            _logger = logger;
            _cartProductRepository = cartProductRepository;
        }
       
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("CartItems")]
        public IActionResult CartItems()
        {
            try
            {
                List<CartProducts> cartProducts = new List<CartProducts>();
                IEnumerable<CartProducts> cartItems = cartProducts;
                cartItems = _cartProductRepository.GetCartProducts(1);//Need to get the UserId from Login Info                                
                return View(cartItems);
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpPost("UpdateCart")]
        public IActionResult UpdateCart(List<CartProducts> cartProducts)
        {
            try
            {
                foreach (var cartItem in cartProducts)
                {
                    _cartProductRepository.UpdateProduct(cartItem.product.Id, cartItem.User.Id, cartItem.Count);
                }
                return Json(new { success = true, message = "Success" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }

            List<CartProducts> cartPUpdate = new List<CartProducts>();
            foreach (var cartItem in cartProducts)
            {
                cartPUpdate.Add(
                new CartProducts
                {
                    Count = cartItem.Count,
                    Id = cartItem.Id,
                    product = cartItem.product,
                    User = cartItem.User
                });
            }
        }

        [HttpGet("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
