using Domain.EntityFramework;
using Domain.Model.Cart;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Cart
{
    public class CartProductRepository : ICartProductRepository
    {
        IGenericRepository<CartProducts> cartProductRepository;
        ApplicationDbContext db = new ApplicationDbContext();
        public CartProductRepository(IGenericRepository<CartProducts> _cartProductRepository)
        {
            this.cartProductRepository = _cartProductRepository;
        }

        public IEnumerable<CartProducts> GetCartProducts(int userId)
        {
            var result = from cp in db.CartProducts
                      join u in db.Register on cp.User.Id equals u.Id
                      join pl in db.Productlst on cp.product.Id equals pl.Id
                      where cp.product.InCart == true
                      select new CartProducts
                      {
                          Id = cp.Id,
                          Count=cp.Count,
                          User=u,
                          product=pl
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

        public void CheckOut()
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int productId, int userId)
        {
            throw new NotImplementedException();
        }

        public void EmptyCart()
        {
            throw new NotImplementedException();
        }

        public int GetCartCount()
        {
            throw new NotImplementedException();
        }

        public decimal GetCartTotal()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(int productId, int userId, int count, string addRemove)
        {
            throw new NotImplementedException();
        }
    }
}
