using Domain.Model.Cart;
using Domain.Model.User;
using System.Collections.Generic;
namespace Services.Cart
{
    public interface ICartProductRepository 
    {
        IEnumerable<CartProducts> GetCartProducts(int userId);//getAllProduct where incart = 1
        void UpdateProduct(long productId, long userId, int count);//Change count
        void DeleteProduct(int productId, int userId);
        void EmptyCart(int userId);//Not needed for now 
        void Checkout(long userId,long addressId);//Insert into Order details table
        List<Address> LoadUserAddress(long userId);
        void SaveUserAddress(Address address, long userId);

    }
}