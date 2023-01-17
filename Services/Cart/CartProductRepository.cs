using Domain.EntityFramework;
using Domain.Model.Cart;
using Domain.Model.Order;
using Domain.Model.User;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
            IEnumerable<CartProducts> cartItems = result?.ToList();
            return cartItems;
        }

        public void DeleteProduct(int productId, int userId)
        {
            //Query to fetch item from cart table
            var recordsToDel = from pc in db.CartProducts
                               where pc.product.Id == productId && pc.User.Id == userId
                               select new { pc }.pc;

            //Query to fetch item from product table
            var productToUpdate = from pl in db.Productlist
                                  where pl.Id == productId
                                  select new { pl }.pl;

            //Update table product list to enable button Add to Cart in Dashboard
            productToUpdate.FirstOrDefault().InCart = false;
            db.Productlist.Update(productToUpdate.FirstOrDefault());

            //Remove product from table CartProducts
            db.CartProducts.Remove(recordsToDel.FirstOrDefault());
            db.SaveChanges();
        }

        public void EmptyCart(int userId)
        {
            //Query to fetch item from cart table
            List<CartProducts> recordsToDel = db.CartProducts.Where(cp => cp.User.Id == userId).ToList();

            //Query to fetch item from product table
            var productToUpdate = from pl in db.Productlist
                                  select new { pl };
            //Update productlist
            foreach (var item in productToUpdate)
            {
                item.pl.InCart = false;
                db.Productlist.Update(item.pl);
            };
            //remove items from cart
            db.CartProducts.RemoveRange(recordsToDel);
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
        public void Checkout(long userId, long addressId)//Moving all products from cart->order tables
        {
            try
            {
                int orderNo = 1;
                if (dbTemp.OrderHeader.Any())
                {
                    orderNo = (dbTemp.OrderHeader.OrderByDescending(u => u.orderNumber).FirstOrDefault().orderNumber) + 1;
                }

                var cartData =
                     from cp in db.CartProducts
                     join pl in db.Productlist on cp.product.Id equals pl.Id
                     join us in db.Register on cp.User.Id equals us.Id
                     join ua in db.Address on new { us.Id, addressId = addressId } equals new { ua.user.Id, addressId = ua.Id }
                     where cp.User.Id == userId
                     select new { cp, pl, us, ua };

                OrderHeader orderHeader = new OrderHeader();
                var itemH = cartData.FirstOrDefault();
                orderHeader.orderNumber = orderNo;
                orderHeader.User = itemH.us;
                orderHeader.orderDate = DateTime.Now;
                orderHeader.Address = itemH.ua;
                db.OrderHeader.Add(orderHeader);
                db.SaveChanges();

                foreach (var item in cartData)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderHeader = orderHeader;
                    orderDetail.count = item.cp.Count;
                    orderDetail.Product = item.pl;
                    db.OrderDetail.Add(orderDetail);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Address> LoadUserAddress(long userId)
        {
            try
            {
                List<Address> addresses = new List<Address>();
                var addressData =
                     from us in db.Register
                     join ad in db.Address on us.Id equals ad.user.Id
                     where us.Id == userId
                     select new { ad };

                foreach (var item in addressData)
                {
                    addresses.Add(item.ad);
                }
                return addresses;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SaveUserAddress(Address addressData, long userId)
        {
            try
            {
                var userData =
                     from us in db.Register
                     where us.Id == userId
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
