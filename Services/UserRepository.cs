using Domain.Model.User;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserRepository : IUserRepository
    {
        IGenericRepository<User> userRepository;



        public UserRepository(IGenericRepository<User> _userRepository)
        {
            this.userRepository = _userRepository;
        }
        public void DeleteUser(long Id)
        {

            User user = GetUser(Id);
            userRepository.Remove(user);
            userRepository.Savechanges();
        }



        public User GetUser(long Id)
        {
            return userRepository.GetT(Id);
        }



        public IEnumerable<User> GetUsers()
        {
            return userRepository.GetAll();
        }



        public int InsertUser(User user)
        {
            userRepository.Insert(user);
            return 1;
        }



        public void UpdatetUser(User user)
        {
            userRepository.Update(user);
        }

        void IUserRepository.InsertUser(User user)
        {
            userRepository.Insert(user);
        }
    }
}