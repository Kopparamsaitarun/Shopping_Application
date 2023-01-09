using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.OrderDetails
{
    public class UserOrderDetails:BaseEntity
    {
        public string EmailId { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string ProductImage { get; set; }
        public int Count { get; set; }
        public bool Isubmitted { get; set; }
    }
}
