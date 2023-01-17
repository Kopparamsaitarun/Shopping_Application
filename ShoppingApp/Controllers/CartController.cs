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
        /// <summary>
        /// Loading all the cart products
        /// </summary>
        /// <returns></returns>
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
        public IActionResult UpdateCart(List<CartProductsDTO> cartProducts)//Updating the item count in cart
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
        public IActionResult Checkout(OrderDetailDTO orderDetail)//Placing the order and moving the data to OrderHeader and OrderDetails
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
        public IActionResult RemoveProductFromCart(CartProductsDTO cartProducts)//Remove particular product from cart 
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

        [HttpGet("LoadUserAddress")]
        public IActionResult LoadUserAddress() //User have multiple addresses loading that all to dropdown
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
        public IActionResult SaveAddress(Address address)//From the modal address saving into Address table
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
        public IActionResult CheckoutSuccess()//After placing order showing success message and redirect to past orders
        {
            ViewBag.userName = "Sangeeth";//Sangeeth UserName hardcoded need to change this
            return View("CheckoutSuccess");
        }
    }
}
