using Domain.Model.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Login
{
    public  interface IUserAuthenticationReposiroty
    {
        string GenerateSessionJWT(UserLogin userInfo);
        string GeneratePasswordResetJWT(UserLogin userInfo);
        string GenerateJSONWebToken(UserLogin userInfo, DateTime ExpireTime);
    }
}
