using Domain.Model.Cart;
using Infrastructure.Repository;
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

        public CartProductRepository(IGenericRepository<CartProducts> _cartProductRepository)
        {
            this.cartProductRepository = _cartProductRepository;
        }

        public IEnumerable<CartProducts> GetCartProducts(int userId)
        {
            List<CartProducts> cartProducts = new List<CartProducts>();
            cartProductRepository.GetAll().ToList().ForEach(u =>
            {
                CartProducts product = null;
                product = new CartProducts()
                {
                   Id=u.Id,
                   Count=u.Count,
                   product=u.product,
                   User=u.User
                };
                cartProducts.Add(product);
            });
            IEnumerable<CartProducts> cartItems = cartProducts;
            return cartItems;
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
