using Domain.Model.Login;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;

namespace ShoppingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerLoginController : Controller
    {
        public ILoginRepository LoginRepository;

        public CustomerLoginController(ILoginRepository _loginRepository)
        {
            LoginRepository = _loginRepository;
        }

        [HttpGet("GetLoginCustomer")]
        public IActionResult GetLoginCustomer()
        {
            return View("~/Views/CustomerLogin/Login.cshtml");
        }

        [HttpPost("LoginCustomer")]
        public IActionResult LoginCustomer(UserLogin model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Model is not valid");
                }
                else
                {
                    bool result = LoginRepository.Login(model);                                                                               
                    return this.Ok(new { Success = true, Message = "Login Successfully" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, ex.Message });
            }
        }
            
           

        }
    }

