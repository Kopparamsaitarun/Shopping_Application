using Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Login
{
    public interface ILoginRepository
    {
        User GetUserByEmail(string email, string password);
    }
}
