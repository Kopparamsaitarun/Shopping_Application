using Domain.Model.Dashboard;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.Model.Order
{
    class OrderDetail:BaseEntity
    {
        public Productlist Product { get; set; }
        public User.User User { get; set; }
        public int count { get; set; }
        [Timestamp]//In SQL DB date will insert automatically
        public DateTime orderDate { get; set; }
    }
}
