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

        /// <summary>
        /// Listing all users registerd for admin role - not in use
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ListUsers()
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Loading the empty Register view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Registration(int id = 0)
        {
            try
            {
                User user = new User();
                string[] roles = { Role.Admin, Role.Customer };
                ViewData["roles"] = roles;
                return View(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// After registration showing success message
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RegistrationSuccess()
        {
            return View("UserRegistrationSuccess");
        }

        /// <summary>
        /// Save the user info into table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(User user)
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

                    var inserted = iuserRepository.InsertUser1(loginUser);
                    if (inserted == 1)
                    {
                        return Json(new { success = true, message = "Success" });
                    }
                    else
                    {
                        return Json(new { success = true, message = "Email already exists" });
                    }
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

        /// <summary>
        /// Updating the user information
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("UpdateUser")]
        public int UpdateUser(User model)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete a particular user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUser")]
        public int DeleteUser(long id)
        {
            try
            {
                iuserRepository.DeleteUser(id);
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check email already exists from the new registration window
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EmailExists(User model)
        {
            try
            {
                if (iuserRepository.EmailExists(model.email))
                {
                    return Json(new { success = true, message = "Email already exists" });
                }
                else
                {
                    return Json(new { success = false, message = "Email Availabe!" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}