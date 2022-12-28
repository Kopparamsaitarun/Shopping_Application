using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Services.Dashboard;
using Domain.Model.Dashboard;
using ShoppingApp.Models.Dashboard;

namespace ShoppingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        [HttpGet("GetProduct")]
        public IActionResult GetProduct()
        {

            try
            {
                List<Productlist> productlist = new List<Productlist>();
                IEnumerable<Productlist> products = productlist;
                products = _dashboardRepository.GetAllProduct();

                return View( products);


            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }

        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct(Productlist model)
        {
            try
            {

                _dashboardRepository.InsertProduct(model);
                return View("", true);
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }

        }

        [HttpDelete("DeleteProduct")]
        public void DeleteProduct(long id)
        {
            try
            {
                _dashboardRepository.DeleteProduct(id);
                GetProduct();

            }
            catch (Exception exception)
            {

            }
        }



    }
}
