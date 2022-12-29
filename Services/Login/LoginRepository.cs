using Domain.Model.Login;
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
        public bool Login(UserLogin userLogin)
        {
            return genericRepository.Login(userLogin);
        }
    }
}
