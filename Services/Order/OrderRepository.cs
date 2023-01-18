using Domain.EntityFramework;
using Domain.Model.Dashboard;
using Domain.Model.Order;
using Infrastructure.Repository;
using ShoppingApp.Models.Cart;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Services.Order
{
    public class OrderRepository : IOrderRepository
    {
        IGenericRepository<OrderDTO> orderRepository;
        ApplicationDbContext db = new ApplicationDbContext();
        public OrderRepository(IGenericRepository<OrderDTO> _orderRepository)
        {
            this.orderRepository = _orderRepository;
        }
        /// <summary>
        /// Get all the user order details from table header and details
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns>List of orders along with product details and address</returns>
        public IEnumerable<OrderDTO> GetOrders(int _userId)
        {
            var result = from oh in db.OrderHeader
                         join us in db.Register on oh.User.Id equals us.Id
                         join ad in db.Address on new { us.Id, addressId=oh.Address.Id } equals new { ad.user.Id, addressId=ad.Id }
                         where oh.User.Id == _userId 
                         select new { oh, us, ad};
            List<OrderDTO> orderList = new List<OrderDTO>();
            //Loading product details to map into the order
            var proLst = 
                from plst in db.Productlist
                join odt in db.OrderDetail on plst.Id equals odt.Product.Id
                join oh in db.OrderHeader on odt.OrderHeader.Id equals oh.Id
                select new { plst,odt,oh };
            //Adding item count against item
            foreach (var temp in proLst)
            {
                temp.plst.Quantity = temp.odt.count;
            };

            foreach (var item in result)
            {
                orderList.Add(
                new OrderDTO
                {
                    Address = item.ad,
                    ProductList = new List<Productlist>(),
                    User = item.us,
                    orderDate = item.oh.orderDate,
                    orderNumber = item.oh.orderNumber,
                    count = 0,
                    orderHeaderId = item.oh.Id,
                    addressId = item.ad.Id,
                    userId = item.us.Id
                }); ;
            };
            //Adding items to order 
            foreach (var t in proLst)
            {
                foreach (var s in orderList)
                {
                    if (t.oh.Id == s.orderHeaderId)
                    {                        
                        s.ProductList.Add(t.plst);                        
                    }
                }
            }
            //Calculating total for using html
            foreach (var t in orderList)
            {
                double tot = 0;
                foreach (var item in t.ProductList)
                {
                     tot += item.ProductPrice;
                }
                t.totalAmount = tot;
            }
            //Sorting the orders by inserted Id desc
            return orderList.OrderByDescending(ord=> ord.orderHeaderId);
        }
    }
}
