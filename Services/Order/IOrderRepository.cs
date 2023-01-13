using Domain.Model.Order;
using ShoppingApp.Models.Cart;
using System.Collections.Generic;
namespace Services.Order
{
    public interface IOrderRepository
    {
        IEnumerable<OrderDTO> GetOrders(int userId);
    }
}
