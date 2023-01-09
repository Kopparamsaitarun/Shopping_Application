using Domain.Model.OrderDetails;
using Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IOrderDetailsRepository
    {
        int UpdatetOrder(UserOrderDetails orderdetails);
        int DeleteOrder(UserOrderDetails order);
    }
}
