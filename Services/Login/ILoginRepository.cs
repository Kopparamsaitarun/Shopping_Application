using Domain.Model.Login;
using Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ILoginRepository
    {
        UserLogin Login(UserLogin userLogin);
    }
}
