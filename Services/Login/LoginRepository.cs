using Domain.Model.Login;
using Domain.Model.User;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Login
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IGenericRepository<UserLogin> genericRepository;
        public LoginRepository(IGenericRepository<UserLogin> _genericRepository)
        {
            genericRepository = _genericRepository;
        }
        public UserLogin Login(UserLogin userLogin)
        {
            return genericRepository.Login(userLogin);
        }
    }
}
