using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Product
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; } 
        public double ProductPrice { get; set; } 
        public byte[] ProductImage { get; set; }
        public int categoryId { get; set; }     

        public ICollection<ProductCategory> Categories { get; set; }
    }
}
