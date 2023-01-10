using Domain.Model.Dashboard;
namespace Domain.Model.Cart
{
    public class CartProducts : BaseEntity
    {
        public Productlist product { get; set; }        
        public User.User User { get; set; }
        public int Count { get; set; }
    }
}
