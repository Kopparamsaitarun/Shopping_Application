using Domain.Model.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using System.Collections.Generic;
using System.Linq;
using Domain.EntityFramework;
using Microsoft.AspNetCore.Http;
using Infrastructure.Repository;

namespace ShoppingApp.Controllers
{
    //[Route("[controller]")]
    public class UserController : Controller
    {
        IUserRepository iuserRepository;

        public UserController(IUserRepository _iuserRepository)
        {
            this.iuserRepository = _iuserRepository;
        }

        [HttpGet]
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
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            User user = new User();
            return View(user);
        }
         
        [HttpPost]
        public ActionResult Register(User user)
        {
            IGenericRepository<User> genericRepository =null;
            UserRepository userRepository = new UserRepository(genericRepository);
            return Json(userRepository.InsertUser(user));
        }
        //[HttpPost]
        //public ActionResult AddOrEdit(User user)
        //{
        //   // User user = new Domain.Model.User.User();

        //    DbContextOptions<ApplicationDbContext> options = new DbContextOptions<ApplicationDbContext>();
        //    ApplicationDbContext db = new ApplicationDbContext(options);

        //    if (db.Register.Any(x => x.email == user.email))
        //    {
        //        ViewBag.UserExistMessage = "User with the Email Already Exists.";
        //        return View("AddOrEdit", user);
        //    }
        //    else
        //    {
        //        db.Register.Add(user);
        //        db.SaveChanges();
        //    }

        //    ModelState.Clear();
        //    ViewBag.SuccessMessage = "User Registration Successful.";
        //    return View("AddOrEdit", new User());
        //}


        [HttpPost("SignUp")]
        public IActionResult SignUp(ShoppingApp.Models.User model)
        {
            //User model = new User();
            User UserEntity = new User
            {
                Id=11,
                firstName = model.firstName,
                lastName = model.lastName,
                email = model.email,
                phoneNumber = model.phoneNumber,
                password = model.password,
                policyFlag = model.policyFlag,
                Role = model.Role,
            };
             iuserRepository.InsertUser(UserEntity);
            return Ok("Success");
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