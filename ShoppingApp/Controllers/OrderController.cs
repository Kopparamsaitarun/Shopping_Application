using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Order;
using ShoppingApp.Models.Cart;
using System.Collections.Generic;
namespace ShoppingApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderRepository _orderRepository;

        public OrderController(ILogger<OrderController> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Loading all orders placed by user
        /// </summary>
        /// <returns> List of orders </returns>
        [HttpGet("GetOrders")]
        public ActionResult GetOrders()
        {
            try
            {
                List<OrderDetailDTO> orders = new List<OrderDetailDTO>();
                var ordersList = _orderRepository.GetOrders(1);//Sangeeth UserId hardcoded need to change this
                return View("~/Views/Order/Orders.cshtml", ordersList);
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
