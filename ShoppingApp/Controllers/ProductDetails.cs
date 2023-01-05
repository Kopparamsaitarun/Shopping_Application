using Microsoft.AspNetCore.Mvc;

namespace ShoppingApp.Controllers
{
    public class ProductDetails : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
