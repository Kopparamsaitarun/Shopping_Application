using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.DashBoard;

namespace ShoppingApp.Controllers
{
  [Route("[controller]")]
    public class DashBoardController : Controller
    {
        IDashboardRepository _dashboardRepository;
        public DashBoardController(IDashboardRepository dashboardRepository)
        {
            this._dashboardRepository = dashboardRepository;
        }
        [HttpGet("GetProductList")]
        public IActionResult GetProductList(long id)
        {
            Productlst productlst=new Productlst(); 
            if (ModelState.IsValid)
            {
                productlst = _dashboardRepository.GetProduct(id);    
            }
            return View("~/Views/DashboardProduct/Productlst.cshtml",productlst);
        }
    }
}