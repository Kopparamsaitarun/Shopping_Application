using Domain.Model;
using Domain.Model.Dashboard;
using Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dashboard
{
    public interface IDashboardRepository
    {
        IEnumerable<Productlist> GetAllProduct();
        IEnumerable<Productlist> GetProductLowHigh();
        IEnumerable<Productlist> GetProductHighLow();
        IEnumerable<Productlist> GetSearchItems(string searchString);
        void InsertProduct(ProductlistModel entity);
        Productlist GetProduct(long id);
        void DeleteProduct(long id);
        void AddtoCart(long id);
    }

}
