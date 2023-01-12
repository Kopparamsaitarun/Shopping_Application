using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApp.Models.Cart
{
    public class OrderDetailDTO
    {
        public int userId { get; set; }
        public int productId { get; set; }
        public int count { get; set; }
        public DateTime orderDate { get; set; }
        public int orderNumber { get; set; }
        public int addressId { get; set; }
    }
}
