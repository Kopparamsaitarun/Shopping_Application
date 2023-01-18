using Domain.Model.User;
using Microsoft.AspNetCore.Mvc;
using Services.Registration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingApp.Controllers
{
    public class UserController : Controller
    {
        IUserRepository iuserRepository;

        public UserController(IUserRepository _iuserRepository)
        {
            this.iuserRepository = _iuserRepository;
        }

        [HttpGet]
        public ActionResult ListUsers()//Listing all users registerd for admin role
        {
            List<User> lstUser = new List<User>();
            iuserRepository.GetUsers().ToList().ForEach(u =>
            {
                User user = null;
                user = new User()
                {
                    Id = u.Id,
                    firstName = u.firstName,
                    lastName = u.lastName,
                    email = u.email,
                    phoneNumber = u.phoneNumber,
                    password = u.password,
                    policyFlag = u.policyFlag,
                    Role = u.Role,
                };
                lstUser.Add(user);
            });

            ViewData["lstUser"] = lstUser;
            return View();
        }
        [HttpGet]
        public ActionResult Registration(int id = 0)//Loading the register view
        {
            User user = new User();
            string[] roles = { Role.Admin, Role.Customer };
            ViewData["roles"] = roles;
            return View(user);
        }

        [HttpGet]
        public IActionResult RegistrationSuccess()//After registration showing success message
        {
            return View("UserRegistrationSuccess");
        }

        [HttpPost]
        public ActionResult Register(User user)//Save the user into table
        {
            try
            {
                if (user.email != "" && user.firstName != "" && user.email != null && user.firstName != null && user.password != "" && user.password != null && user.password == user.ConfirmPassword)
                {
                    User loginUser = new User
                    {
                        firstName = user.firstName,
                        ConfirmPassword = user.ConfirmPassword,
                        email = user.email,
                        lastName = user.lastName,
                        password = Models.EncDec.Encrypt(user.password),
                        Role = user.Role,
                        phoneNumber = user.phoneNumber,
                        policyFlag = user.policyFlag
                    };

                    iuserRepository.InsertUser(loginUser);
                    return Json(new { success = true, message = "Success" });
                }
                else
                {
                    return Json(new { success = false, message = "Fill all mandatory fields" });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { success = false, exception.Message });
            }
        }


        [HttpPut("UpdateUser")]
        public int UpdateUser(User model)
        {
            User UserEntity = new User
            {
                Id = model.Id,
                firstName = model.firstName,
                lastName = model.lastName,
                email = model.email,
                phoneNumber = model.phoneNumber,
                password = model.password,
                policyFlag = model.policyFlag,
                Role = model.Role,

            };
            iuserRepository.UpdatetUser(UserEntity);
            return 1;
        }

        [HttpDelete("DeleteUser")]
        public int DeleteUser(long id)
        {
            iuserRepository.DeleteUser(id);
            return 1;
        }

        [HttpGet]
        public ActionResult EmailExists(User model)
        {
            try
            {
                if (iuserRepository.EmailExists(model.email))
                {
                    return Json(new { success = true, message = "Email already exists" });}
                else
                {
                    return Json(new { success = false, message = "Email Availabe!" });}                     
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}