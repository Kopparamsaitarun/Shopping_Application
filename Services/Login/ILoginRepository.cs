using Domain.Model.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ILoginRepository
    {
        bool Login(UserLogin userLogin);
    }
}
