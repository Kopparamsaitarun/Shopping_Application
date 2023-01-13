using Domain.Model.Dashboard;
namespace Domain.Model.Order
{
    public class OrderDetail:BaseEntity
    {
        public OrderHeader OrderHeader { get; set; }
        public Productlist Product { get; set; }
        public int count { get; set; }
    }
}
