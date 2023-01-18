using Microsoft.AspNetCore.Mvc;
using Services.Login;
using System.Collections.Generic;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Services;
using ShoppingApp.Models;
using Services.Dashboard;
using Domain.Model.Dashboard;
using Microsoft.AspNetCore.Hosting;

namespace ShoppingApp.Controllers
{
    public class LoginController : Controller
    {
        public ILoginRepository LoginRepository;
        private readonly IDashboardRepository dashboardRepository;

        public LoginController(ILoginRepository _loginRepository, IDashboardRepository _dashboardRepository)
        {
            LoginRepository = _loginRepository;
            dashboardRepository = _dashboardRepository;
        }
        /// <summary>
        /// Return the view of Login cshtml where we are taking email and password
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LoginCustomer()
        {
            try
            {
                return View("~/Views/Login/LoginCustomer.cshtml");
            }
            catch(Exception ex) 
            {
                return BadRequest(new { success = false, ex.Message });

            }
        }

        /// <summary>
        /// Post method of login where conver the password in encrypted format then call method from loginrepository
        /// Then call token generation method and then call getAllproduct from dashboardrepository and return the dashboard view
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult LoginCustomer(string email, string password)
        {
            try
            {
                string passwd = EncDec.Encrypt(password);

                var user = LoginRepository.GetUserByEmail(email, passwd);
                if (user != null)
                {
                    var token = GenerateJwtToken(user.email, user.Id, user.Role);
                    HttpContext.Session.SetString("token", token);
                    ViewBag.Token = token;
                    List<Productlist> productlist = new List<Productlist>();
                    IEnumerable<Productlist> products = productlist;
                    products = dashboardRepository.GetAllProduct();

                    return View("~/Views/Dashboard/Product.cshtml", products);
                }
                return BadRequest("User Not found");
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, ex.Message });

            }
        }

        /// <summary>
        /// Retriving the email id and role from post login method and then using Jwt token process generate the token 
        /// By token provide role based authrization 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private string GenerateJwtToken(string email, long id, string role)
        {
            
            //JWT is created with a secret key and that secret key is private to you which means you will never reveal that to the public or inject inside the JWT token
            var securityKey = Encoding.UTF8.GetBytes("this is my custom Secret key for authentication");
            //identifies principal that issued the JWT
            var issuer = "TokenAuthDemo";
            //A claim is represented as a name-value pair that contains a Claim Name and a Claim Value. 
            var claims = new[]
                {
                new Claim(ClaimTypes.Role, role),
                new Claim("Email", email),
                new Claim("Id", id.ToString())
            };
            // SigningCredentials=>the secret key used to both encrypt and decrypt your secure payload
            //SymmetricSecurityKey=>Determines whether the specified object is equal to the current object
            //SecurityAlgorithms=>HmacSha256 is a string constant evaluating to "HS256
            var credentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    issuer,
                    issuer,
                    claims,
                    expires: DateTime.Now.AddDays(10),
                    signingCredentials: credentials
                );
            //JwtSecurityTokenHandler=>A SecurityTokenHandler designed for creating and validating Json Web Tokens.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Logout method where clear the session token 
        /// </summary>
        /// <returns></returns>
        [HttpGet("LogoutCustomer")]
        public IActionResult LogoutCustomer()
        {
            try
            {
                //Clear the token from the session not to use in further
                HttpContext.Session.Clear();
                return View("~/Views/Login/LoginCustomer.cshtml");
            }
            catch(Exception ex)
            {
                return BadRequest(new { success = false, ex.Message });

            }
        }



    }
}
