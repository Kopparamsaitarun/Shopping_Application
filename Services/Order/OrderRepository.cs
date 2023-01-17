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
        public IEnumerable<OrderDTO> GetOrders(int _userId)//Loadind order header and details
        {
            var result = from oh in db.OrderHeader
                         join us in db.Register on oh.User.Id equals us.Id
                         join ad in db.Address on new { us.Id, addressId=oh.Address.Id } equals new { ad.user.Id, addressId=ad.Id }
                         where oh.User.Id == _userId 
                         select new { oh, us, ad};
            List<OrderDTO> orderList = new List<OrderDTO>();

            var proLst = //Loading product details to map into the order
                from plst in db.Productlist
                join odt in db.OrderDetail on plst.Id equals odt.Product.Id
                join oh in db.OrderHeader on odt.OrderHeader.Id equals oh.Id
                select new { plst,odt,oh };

            foreach(var temp in proLst)//Adding item count against item
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

            foreach (var t in proLst)//Adding items to order 
            {
                foreach (var s in orderList)
                {
                    if (t.oh.Id == s.orderHeaderId)
                    {                        
                        s.ProductList.Add(t.plst);                        
                    }
                }
            }

            foreach (var t in orderList)//Calculating total for passing to html
            {
                double tot = 0;
                foreach (var item in t.ProductList)
                {
                     tot += item.ProductPrice;
                }
                t.totalAmount = tot;
            }

            return orderList.OrderByDescending(ord=> ord.orderHeaderId);//Sorting the orders by inserted Id desc

        }

        public IEnumerable<OrderHeader> GetOrderHeader(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
