using Domain.Model.User;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Login
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IUserRepository userRepository;
        public LoginRepository(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        

        public User GetUserByEmail(string email, string password)
        {
            //Retriving all user by calling method from userrepository
            //Using linq match the email and password combination present in table or not
            var allUsers = userRepository.GetUsers();
            var user = allUsers.SingleOrDefault(x => (x.email == email && x.password == password));
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}
