using Domain.Model.OrderDetails;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        IGenericRepository<UserOrderDetails> orderRepository;

        public OrderDetailsRepository(IGenericRepository<UserOrderDetails> _orderRepository)
        {
            orderRepository = _orderRepository;
        }

        public int DeleteOrder(UserOrderDetails order)
        {
            return 1;
        }

        public int UpdatetOrder(UserOrderDetails orderdetails)
        {
            return 1;
        }
    }
}
