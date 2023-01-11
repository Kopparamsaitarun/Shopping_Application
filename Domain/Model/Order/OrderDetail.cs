using Domain.Model.Dashboard;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.Model.Order
{
    public class OrderDetail:BaseEntity
    {
        public int orderNumber { get; set; }
        public Productlist Product { get; set; }
        public User.User User { get; set; }
        public int count { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime orderDate { get; set; }
    }
}
