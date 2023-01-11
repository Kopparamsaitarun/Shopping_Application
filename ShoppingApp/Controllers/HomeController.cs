using Domain.EntityFramework;
using Domain.Model.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Cart;
using ShoppingApp.Models;
using ShoppingApp.Models.Cart;
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
                cartItems = _cartProductRepository.GetCartProducts(1);//Sangeeth UserId hardcoded need to change this                            
                return View(cartItems);
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpPost("UpdateCart")]
        public IActionResult UpdateCart(List<CartProductsDTO> cartProducts)
        {
            try
            {
                foreach (var cartItem in cartProducts)
                {
                    _cartProductRepository.UpdateProduct(cartItem.productId, cartItem.userId, cartItem.count);
                }
                return Json(new { success = true, message = "Success" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpPost("Checkout")]
        public IActionResult Checkout()
        {
            try
            {
                _cartProductRepository.Checkout(1);//Sangeeth UserId hardcoded need to change this
                _cartProductRepository.EmptyCart(1);//Sangeeth UserId hardcoded need to change this
                return Json(new { success = true, message = "Success" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
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
