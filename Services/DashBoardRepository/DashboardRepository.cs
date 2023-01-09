using Domain.Model;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DashBoard
{
    public class DashboardRepository : IDashboardRepository
    {
        IGenericRepository<Productlst>   genericRepository;

        public DashboardRepository(IGenericRepository<Productlst> _genericRepository)
        {
            this.genericRepository = _genericRepository;
        }
        public Productlst GetProduct(long id)
        {
            return genericRepository.GetT(id);
        }
    }
}
