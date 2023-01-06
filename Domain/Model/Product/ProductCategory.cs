using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Product
{
    public class ProductCategory :BaseEntity
    {
        public Product product { get; set; }
        public Category category { get; set; }

    }
}
