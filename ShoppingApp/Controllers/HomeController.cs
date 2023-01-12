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
