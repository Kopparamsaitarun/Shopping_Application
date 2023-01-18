using Domain.Model.User;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Registration
{

    public class UserRepository : IUserRepository
    {
        IGenericRepository<User> userRepository;

        public UserRepository(IGenericRepository<User> _userRepository)
        {
            userRepository = _userRepository;
        }
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteUser(long Id)
        {
            try
            {
                User user = GetUser(Id);
                userRepository.Remove(user);
                userRepository.Savechanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To check the new user email exists
        /// </summary>
        /// <param name="emailInput"></param>
        /// <returns></returns>
        public bool EmailExists(string emailInput)
        {
            try
            {
                return userRepository.GetAll().Where(e => e.email?.ToLower() == emailInput?.ToLower()).Any();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public User GetUser(long Id)
        {
            try
            {
                return userRepository.GetT(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get list of users
        /// </summary>
        /// <returns> list of users </returns>
        public IEnumerable<User> GetUsers()
        {
            try
            {
                return userRepository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Added InsertUser1 with email existing() check
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int InsertUser1(User user)
        {
            if (!EmailExists(user.email))
            {
                userRepository.Insert(user);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Update the user info
        /// </summary>
        /// <param name="user"></param>
        public void UpdatetUser(User user)
        {
            try
            {
                userRepository.Update(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Insert user data into table register
        /// </summary>
        /// <param name="user"></param>
        void IUserRepository.InsertUser(User user)
        {
            try
            {
                userRepository.Insert(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}