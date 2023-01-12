using Domain.EntityFramework;
using Domain.Model.Cart;
using Domain.Model.Dashboard;
using Domain.Model.Order;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.User;

namespace Services.Cart
{
    public class CartProductRepository : ICartProductRepository
    {
        IGenericRepository<CartProducts> cartProductRepository;
        ApplicationDbContext db = new ApplicationDbContext();
        ApplicationDbContext dbTemp = new ApplicationDbContext();
        public CartProductRepository(IGenericRepository<CartProducts> _cartProductRepository)
        {
            this.cartProductRepository = _cartProductRepository;
        }

        public IEnumerable<CartProducts> GetCartProducts(int userId)
        {
            var result = from cp in db.CartProducts
                         join u in db.Register on cp.User.Id equals u.Id
                         join pl in db.Productlist on cp.product.Id equals pl.Id
                         where cp.product.InCart == true
                         select new CartProducts
                         {
                             Id = cp.Id,
                             Count = cp.Count,
                             User = u,
                             product = pl
                         };
            IEnumerable<CartProducts> cartItems = result.ToList();
            return cartItems;

            //Need to check this later - Sangeeth
            //List<CartProducts> cartProducts = new List<CartProducts>();
            //cartProductRepository.GetAll().ToList().ForEach(u =>
            //{
            //    CartProducts product = null;
            //    product = new CartProducts()
            //    {
            //        Id = u.Id,
            //        Count = u.Count,
            //        product = u.product,
            //        User = u.User
            //    };
            //    cartProducts.Add(product);
            //});
        }

        public void DeleteProduct(int productId, int userId)
        {
            var recordsToDel = from pc in db.CartProducts
                               where pc.product.Id == productId && pc.User.Id == userId
                               select new { pc };
            foreach (var item in recordsToDel)
            {
                db.CartProducts.Remove(item.pc);
            };
            db.SaveChanges();
        }

        public void EmptyCart(int userId)
        {
            var recordsToDel = from pc in db.CartProducts
                               where pc.User.Id == userId
                               select new { pc };
            foreach (var item in recordsToDel)
            {
                db.CartProducts.Remove(item.pc);
            };
            db.SaveChanges();
        }

        public void UpdateProduct(long productId, long userId, int count)
        {
            try
            {
                var query =
                    from cp in db.CartProducts
                    join pl in db.Productlist on cp.product.Id equals pl.Id
                    join us in db.Register on cp.User.Id equals us.Id
                    where cp.product.Id == productId && cp.User.Id == userId
                    select new { cp, cp.product, cp.User };

                CartProducts cartProducts = new CartProducts();
                cartProducts = query.ToList().ElementAt(0).cp;
                cartProducts.Count = count;
                db.CartProducts.Update(cartProducts);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void Checkout(long userId)
        {
            try
            {
                int orderNo = 1;
                if (dbTemp.OrderDetail.Any())
                {
                    orderNo = (dbTemp.OrderDetail.OrderByDescending(u => u.orderNumber).FirstOrDefault().orderNumber) + 1;
                }

                var cartData =
                     from cp in db.CartProducts
                     join pl in db.Productlist on cp.product.Id equals pl.Id
                     join us in db.Register on cp.User.Id equals us.Id
                     where cp.User.Id == userId
                     select new { cp, pl, us };

                foreach (var item in cartData)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.count = item.cp.Count;
                    orderDetail.orderNumber = orderNo;
                    orderDetail.Product = item.pl;
                    orderDetail.User = item.us;
                    orderDetail.orderDate = DateTime.Now;
                    db.OrderDetail.Add(orderDetail);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }

        //public List<Address> LoadUserAddress(long userId)
        //{
        //    try
        //    {
       
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public void SaveUserAddress(Address addressData)
        {
            try
            {
                var userData =
                     from us in db.Register
                     where us.Id == 1
                     select new { us };

                Address address = new Address()
                {
                    postCode = addressData.postCode,
                    address1 = addressData.address1,
                    address2 = addressData.address2,
                    city = addressData.city,
                    country = addressData.country,
                    state = addressData.state,
                    user = userData.ToList().ElementAt(0).us
                };
                db.Address.Add(address);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
