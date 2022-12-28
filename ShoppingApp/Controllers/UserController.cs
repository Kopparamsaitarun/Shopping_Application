using Domain.Model.User;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        IUserRepository iuserRepository;
        public UserController(IUserRepository _iuserRepository)
        {
            this.iuserRepository = _iuserRepository;
        }

        [HttpGet("ListUsers")]
        public ActionResult ListUsers()
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
                    Role=u.Role,
                };
                lstUser.Add(user);
            });
            //return Ok(lstUser);
            ViewData["lstUser"] = lstUser;
            return View();
        }

        [HttpPost("CreateUsers")]
        public int CreateUsers(User model)
        {
            User UserEntity = new User
            {
                firstName = model.firstName,
                lastName = model.lastName,
                email = model.email,
                phoneNumber = model.phoneNumber,
                password = model.password,
                policyFlag = model.policyFlag,
                Role = model.Role,
            };
            iuserRepository.InsertUser(UserEntity);
            return 1;
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

    }
}