using Domain.Model.User;
using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.Model.Order
{
    public class OrderHeader : BaseEntity
    {
        public int orderNumber { get; set; }
        public Address Address { get; set; }
        public User.User User { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime orderDate { get; set; }
    }
}
