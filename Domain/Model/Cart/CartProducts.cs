namespace Domain.Model.Cart
{
    public class CartProducts : BaseEntity
    {
        public Productlst product { get; set; }        
        public User.User User { get; set; }
        public int Count { get; set; }
    }
}
