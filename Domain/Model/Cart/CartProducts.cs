using Domain.Model.Dashboard;

namespace Domain.Model.Cart
{
    public class CartProducts : BaseEntity
    {
        public Productlist product { get; set; }//ProductId
        public User.User User { get; set; }//UserId
        public int Count { get; set; }//Increase Decrease
    }
}
