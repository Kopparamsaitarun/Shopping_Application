using Domain.Model.Cart;
using Domain.Model.User;
using System.Collections.Generic;
namespace Services.Cart
{
    public interface ICartProductRepository 
    {
        IEnumerable<CartProducts> GetCartProducts(int userId);
        void UpdateProduct(long productId, long userId, int count);
        void DeleteProduct(int productId, int userId);
        void EmptyCart(int userId);
        void Checkout(long userId,long addressId);
        List<Address> LoadUserAddress(long userId);
        void SaveUserAddress(Address address, long userId);

    }
}