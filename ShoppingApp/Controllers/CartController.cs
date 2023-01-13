using Domain.Model.Cart;
using Domain.Model.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Cart;
using ShoppingApp.Models.Cart;
using System;
using System.Collections.Generic;

namespace ShoppingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly ICartProductRepository _cartProductRepository;

        public CartController(ILogger<CartController> logger, ICartProductRepository cartProductRepository)
        {
            _logger = logger;
            _cartProductRepository = cartProductRepository;
        }

        public IActionResult Index()
        {
            try
            {
                List<CartProducts> cartProducts = new List<CartProducts>();
                IEnumerable<CartProducts> cartItems = cartProducts;
                cartItems = _cartProductRepository.GetCartProducts(1);//Sangeeth UserId hardcoded need to change this                            
                return View("CartItems",cartItems);
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpGet("CartItems")]
        public IActionResult CartItems()
        {
            try
            {
                List<CartProducts> cartProducts = new List<CartProducts>();
                IEnumerable<CartProducts> cartItems = cartProducts;
                cartItems = _cartProductRepository.GetCartProducts(1);//Sangeeth UserId hardcoded need to change this                            
                ViewBag.addresses = _cartProductRepository.LoadUserAddress(1);
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
                    _cartProductRepository.UpdateProduct(cartItem.productId, 1 , cartItem.count);//Sangeeth UserId hardcoded need to change this
                }
                return Json(new { success = true, message = "Success" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpPost("Checkout")]
        public IActionResult Checkout(OrderDetailDTO orderDetail)
        {
            try
            {
                _cartProductRepository.Checkout(1,orderDetail.addressId);//Sangeeth UserId hardcoded need to change this
                _cartProductRepository.EmptyCart(1);//Sangeeth UserId hardcoded need to change this
                return Json(new { success = true, message = "Success" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpPost("RemoveProductFromCart")]
        public IActionResult RemoveProductFromCart(CartProductsDTO cartProducts)
        {
            try
            {
                _cartProductRepository.DeleteProduct(cartProducts.productId, 1);//Sangeeth UserId hardcoded need to change this
                return Json(new { success = true, message = "Success" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpGet("LoadAddressView")]
        public ActionResult LoadAddressView()
        {
            Address address = new Address();
            return View("~/Views/Cart/AddAddress.cshtml", address);
        }

        [HttpGet("LoadUserAddress")]
        public IActionResult LoadUserAddress()
        {
            try
            {
                List<Address> address = _cartProductRepository.LoadUserAddress(1);//Sangeeth UserId hardcoded need to change this
                return Json(new { address, message = "Success" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpPost("SaveUserAddress")]
        public IActionResult SaveAddress(Address address)
        {
            try
            {   
                _cartProductRepository.SaveUserAddress(address,1);//Sangeeth UserId hardcoded need to change this
                return Json(new { success = true, message = "Success" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpGet("CheckoutSuccess")]
        public IActionResult CheckoutSuccess()
        {
            ViewBag.userName = "Sangeeth";//Sangeeth UserName hardcoded need to change this
            return View("CheckoutSuccess");
        }
    }
}
