using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.DashBoard;

namespace ShoppingApp.Controllers
{
    public class DashBoardController : Controller
    {
        IDashboardRepository _dashboardRepository;
        public DashBoardController(IDashboardRepository dashboardRepository)
        {
            this._dashboardRepository = dashboardRepository;
        }
        [HttpGet]
        public IActionResult GetProductList(long id)
        {
            if (ModelState.IsValid)
            {
                _dashboardRepository.GetProduct(id);
                
            }
            return View("~/Views/DashboardProduct/Productlst.cshtml");
        }
    }
}