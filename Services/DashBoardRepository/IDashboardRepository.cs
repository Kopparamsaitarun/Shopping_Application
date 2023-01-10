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
        IEnumerable<Productlst> GetAllProduct();
        void InsertProduct(Productlst entity);

        Productlst GetProduct(long id);
        void DeleteProduct(long id);
    }
}
