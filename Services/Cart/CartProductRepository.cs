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
        IGenericRepository<CartProducts> _cartProductRepository;
        ApplicationDbContext db = new ApplicationDbContext();
        public CartProductRepository(IGenericRepository<CartProducts> cartProductRepository)
        {
            this._cartProductRepository = cartProductRepository;
        }
        /*
        Usage   : Getting all the products from the CartProducts table
        Input   : Current userId
        Output  : Return List of Products in cart
        */
        public IEnumerable<CartProducts> GetCartProducts(int userId)
        {
            try
            {
                //Query to get all products in cart
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
            catch (Exception)
            {
                throw;
            }
        }
        /*
        Usage   : Delete the product from user cart and
                  enable add to product button in Dashboard
        Input   : Selected product Id, current user Id
        Output  : Return nothing
        */
        public void DeleteProduct(int productId, int userId)
        {
            try
            {
                //Query to fetch products from cartProduct table
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
            catch (Exception)
            {
                throw;
            }
        }
        /*
        Usage   : Delete all the products from user cart
                  Calling after checkout order
        Input   : Current user Id
        Output  : Return nothing
        */
        public void EmptyCart(int userId)
        {
            try
            {
                //Query to fetch all user's products from cart table
                List<CartProducts> recordsToDel = db.CartProducts.Where(cp => cp.User.Id == userId).ToList();

                //Query to fetch all products from product table
                var productToUpdate = from pl in db.Productlist
                                      select new { pl };
                //Update products table setting InCart = false
                //To enable button "Add to Cart" in Dashboard
                foreach (var item in productToUpdate)
                {
                    item.pl.InCart = false;
                    db.Productlist.Update(item.pl);
                };
                //Remove all products from cart table
                db.CartProducts.RemoveRange(recordsToDel);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /*
        Usage   : Update the product count in cart table 
                  In UI + - buttons calling this function
        Input   : Product Id, current user Id, Count in cart
        Output  : Return nothing
        */
        public void UpdateProduct(long productId, long userId, int count)
        {
            try
            {
                //Query to get the user's product from cart table
                var query =
                    from cp in db.CartProducts
                    join pl in db.Productlist on cp.product.Id equals pl.Id
                    join us in db.Register on cp.User.Id equals us.Id
                    where cp.product.Id == productId && cp.User.Id == userId
                    select new { cp, cp.product, cp.User };

                //Updating product count
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

        /*
        Usage   : Moving all products from cart -> order tables
                  there by placing the order
        Input   : Current user Id, user address Id
        Output  : Return nothing
        */
        public void Checkout(long userId, long addressId)
        {
            try
            {
                //Generating the order number; for the first order it will be 1 
                int orderNo = 1;
                if (db.OrderHeader.Any())
                {
                    orderNo = (db.OrderHeader.OrderByDescending(u => u.orderNumber).FirstOrDefault().orderNumber) + 1;
                }
                //Query to get all the product from cart
                var cartData =
                     from cp in db.CartProducts
                     join pl in db.Productlist on cp.product.Id equals pl.Id
                     join us in db.Register on cp.User.Id equals us.Id
                     join ua in db.Address on new { us.Id, addressId = addressId } equals new { ua.user.Id, addressId = ua.Id }
                     where cp.User.Id == userId
                     select new { cp, pl, us, ua };

                //Adding data to OrderHeader table
                OrderHeader orderHeader = new OrderHeader();
                var itemH = cartData.FirstOrDefault();
                orderHeader.orderNumber = orderNo;
                orderHeader.User = itemH.us;
                orderHeader.orderDate = DateTime.Now;
                orderHeader.Address = itemH.ua;
                db.OrderHeader.Add(orderHeader);
                db.SaveChanges();

                //Adding data to OrderDetails table
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
        /*
        Usage   : Loading all the addresses of current User
        Input   : Current user Id
        Output  : Return List of Addresses
        */
        public List<Address> LoadUserAddress(long userId)
        {
            try
            {
                List<Address> addresses = new List<Address>();
                //Query to get all the addresses of user
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
        /*
        Usage   : Save the new address added by user
        Input   : Complete address Data, current user Id
        Output  : Return nothing
        */
        public void SaveUserAddress(Address addressData, long userId)
        {
            try
            {
                //Query to get the current user
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
