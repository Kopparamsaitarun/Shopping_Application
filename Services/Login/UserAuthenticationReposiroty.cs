using Domain.Model.Login;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Login
{
    public  class UserAuthenticationReposiroty :IUserAuthenticationReposiroty
    {
        private readonly IConfiguration config;
        public UserAuthenticationReposiroty(IConfiguration config)
        {
            this.config = config;
        }
        /// <summary>
        /// Generates the session JWT.
        /// </summary>
        /// <param name="userInfo">The user information.</param>
        /// <returns></returns>
        public string GenerateSessionJWT(UserLogin userInfo)
        {
            DateTime ExpireTime = DateTime.Now.AddMinutes(120);
            return GenerateJSONWebToken(userInfo, ExpireTime);
        }
        /// <summary>
        /// Generates the password reset JWT.
        /// </summary>
        /// <param name="userInfo">The user information.</param>
        /// <returns></returns>
        public string GeneratePasswordResetJWT(UserLogin userInfo)
        {
            DateTime ExpireTime = DateTime.Now.AddHours(2);
            return GenerateJSONWebToken(userInfo, ExpireTime);
        }
        /// <summary>
        /// Generates the json web token.
        /// </summary>
        /// <param name="userInfo">The user information.</param>
        /// <param name="ExpireTime">The expire time.</param>
        /// <returns></returns>
        public string GenerateJSONWebToken(UserLogin userInfo, DateTime ExpireTime)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            IEnumerable<Claim> Claims = new Claim[] {
           //     new Clain("FundooNotes", "Notes");
               
                new Claim("EmailId", userInfo.EmailId),
             new Claim("Role", userInfo.Role)};



            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"],
              claims: Claims,
              expires: ExpireTime,
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
