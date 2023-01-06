using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Domain.Model.Cart
{
    public class CartProducts : BaseEntity
    {
        public int userId { get; set; }
        public string productId { get; set; }
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Product.Product product { get; set; }
        public User.User User { get; set; }
    }
}
