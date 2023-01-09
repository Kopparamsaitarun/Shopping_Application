using Microsoft.AspNetCore.Mvc;

namespace ShoppingApp.Controllers
{
    public class ProductControler : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}