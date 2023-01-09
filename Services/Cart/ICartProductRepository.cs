using Domain.Model.Cart;
using System.Collections.Generic;
namespace Services.Cart
{
    public interface ICartProductRepository
    {
        IEnumerable<CartProducts> GetCartProducts(int userId);//getAllProduct where incart = 1
        void UpdateProduct(int productId, int userId, int count, string addRemove);//Change amt
        void DeleteProduct(int productId, int userId);
        void EmptyCart();//Not needed for now 
        int GetCartCount();//Not needed for now
        decimal GetCartTotal();//Not needed for now 
        void CheckOut();//Insert into Order details table
    }
}