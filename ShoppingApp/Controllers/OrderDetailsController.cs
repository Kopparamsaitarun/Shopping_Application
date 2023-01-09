using Domain.Model.OrderDetails;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ShoppingApp.Controllers
{
    public class OrderDetailsController : Controller
    {
        IOrderDetailsRepository orderRepository;

        public OrderDetailsController(IOrderDetailsRepository _orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public IActionResult orderDetails()
        {
            return View();
        }

        [HttpPost]
        public IActionResult orderDetails(UserOrderDetails details)
        {
            return View();
        }
    }
}
