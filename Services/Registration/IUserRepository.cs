using Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Registration
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUser(long Id);
        void InsertUser(User user);
        int InsertUser1(User user);
        void UpdatetUser(User user);
        void DeleteUser(long Id);
        bool EmailExists(string emailInput);
    }
}
