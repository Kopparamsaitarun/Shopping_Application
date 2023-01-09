using Domain.Model;
using Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DashBoard
{
    public interface IDashboardRepository
    {
        Productlst GetProduct(long id);

    }
}
