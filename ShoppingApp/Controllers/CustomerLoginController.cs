using Domain.Model.Login;
using Domain.Model.User;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Login;
using System;

namespace ShoppingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerLoginController : Controller
    {
        public ILoginRepository LoginRepository;
        private readonly IUserAuthenticationReposiroty userAuthenticationReposiroty;

        public CustomerLoginController(ILoginRepository _loginRepository, IUserAuthenticationReposiroty _userAuthenticationReposiroty)
        {
            LoginRepository = _loginRepository;
            userAuthenticationReposiroty = _userAuthenticationReposiroty;
        }

        [HttpGet("GetLoginCustomer")]
        public IActionResult GetLoginCustomer()
        {
            return View("~/Views/CustomerLogin/Login.cshtml");
        }

        [HttpPost("LoginCustomer")]
        public IActionResult LoginCustomer(UserLogin model)
        {
            if (model == null)
            {
                return BadRequest("user is null.");
            }
            try
            {
                UserLogin user = LoginRepository.Login(model);
                if (user != null)
                {
                    var tokenString = userAuthenticationReposiroty.GenerateSessionJWT(user);
                    return Ok(new
                    {
                        success = true,
                        Message = "User Login Successful",
                        user,
                        token = tokenString
                    });
                }
                return BadRequest(new { success = false, Message = "User Login Unsuccessful" });
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }
            
           

        }
    }

