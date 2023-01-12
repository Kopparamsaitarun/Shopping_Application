using Domain.Model.Login;
using Domain.Model.User;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Dashboard;
using Services.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace ShoppingApp.Controllers
{
    [Route("[controller]")]
    public class CustomerLoginController : Controller
    {
        public ILoginRepository LoginRepository;
        private readonly IUserAuthenticationReposiroty userAuthenticationReposiroty;
        public IDashboardRepository dashboardRepository;

        public CustomerLoginController(ILoginRepository _loginRepository, IUserAuthenticationReposiroty _userAuthenticationReposiroty,
            IDashboardRepository _dashboardRepository)
        {
            LoginRepository = _loginRepository;
            userAuthenticationReposiroty = _userAuthenticationReposiroty;
            dashboardRepository = _dashboardRepository;
        }

        [HttpGet("GetLoginCustomer")]
        public IActionResult GetLoginCustomer()
        {

            List<string> roles = new List<string>();
            foreach(var role in Enum.GetNames(typeof(Roles)))
            {
                roles.Add(role);
            }

            return View("~/Views/CustomerLogin/LoginCustomer.cshtml", roles);
        }

        [HttpPost("LoginCustomer")]
        public IActionResult LoginCustomer(UserLogin model)
        {
            if (model == null)
            {
                return BadRequest("user is null");
            }
            
            try
            {
                UserLogin user = LoginRepository.Login(model);
                if (user.Role!= null)
                {
                   dashboardRepository.GetRole(user.Role);
                    var tokenString = userAuthenticationReposiroty.GenerateSessionJWT(user);
                    return Ok(new
                    {
                        success = true,
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

