using Domain.Model.Cart;
using System.Collections.Generic;
namespace Services.Cart
{
    public interface ICartProductRepository 
    {
        IEnumerable<CartProducts> GetCartProducts(int userId);//getAllProduct where incart = 1
        void UpdateProduct(long productId, long userId, int count);//Change count
        void DeleteProduct(int productId, int userId);
        void EmptyCart(int userId);//Not needed for now 
        int GetCartCount();//Not needed for now
        decimal GetCartTotal();//Not needed for now 
        void Checkout(long userId);//Insert into Order details table
    }
}