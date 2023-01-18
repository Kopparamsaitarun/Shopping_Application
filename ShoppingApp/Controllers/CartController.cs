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
                return View("CartItems", cartItems);
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        /// <summary>
        /// Loading all the user products in cart
        /// </summary>
        /// <returns>List of products in cart</returns>
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
        /// <summary>
        /// Updating the item count in user cart
        /// </summary>
        /// <param name="cartProducts"></param>
        /// <returns>Suceess true and false</returns>
        [HttpPost("UpdateCart")]
        public IActionResult UpdateCart(List<CartProductsDTO> cartProducts)
        {
            try
            {
                foreach (var cartItem in cartProducts)
                {
                    _cartProductRepository.UpdateProduct(cartItem.productId, 1, cartItem.count);//Sangeeth UserId hardcoded need to change this
                }
                return Json(new { success = true, message = "Success" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
        /// <summary>
        /// Placing the order and moving the cart data to OrderHeader and OrderDetails
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <returns>Suceess true and false</returns>
        [HttpPost("Checkout")]
        public IActionResult Checkout(OrderDetailDTO orderDetail)
        {
            try
            {
                _cartProductRepository.Checkout(1, orderDetail.addressId);//Sangeeth UserId hardcoded need to change this
                _cartProductRepository.EmptyCart(1);//Sangeeth UserId hardcoded need to change this
                return Json(new { success = true, message = "Success" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        /// <summary>
        /// Remove a single product from cart
        /// </summary>
        /// <param name="cartProducts"></param>
        /// <returns>Suceess true and false</returns>
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

        /// <summary>
        /// User have multiple addresses loading that all in to dropdown
        /// </summary>
        /// <returns> List of address </returns>
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

        /// <summary>
        /// From the modal-address saving data into Address table
        /// </summary>
        /// <param name="address"></param>
        /// <returns> Suceess true and false </returns>
        [HttpPost("SaveUserAddress")]
        public IActionResult SaveAddress(Address address)
        {
            try
            {
                _cartProductRepository.SaveUserAddress(address, 1);//Sangeeth UserId hardcoded need to change this
                return Json(new { success = true, message = "Success" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        /// <summary>
        /// After placing order showing success message and redirect to past orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("CheckoutSuccess")]
        public IActionResult CheckoutSuccess()
        {
            ViewBag.userName = "Sangeeth";//Sangeeth UserName hardcoded need to change this
            return View("CheckoutSuccess");
        }
    }
}
