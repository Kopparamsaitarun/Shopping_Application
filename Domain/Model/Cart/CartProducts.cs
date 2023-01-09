namespace Domain.Model.Cart
{
    public class CartProducts : BaseEntity
    {
        public Product.Product product { get; set; }//ProductId
        public User.User User { get; set; }//UserId
        public int Count { get; set; }//Increase Decrease
    }
}
