using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Dashboard
{
    public class Productlist : BaseEntity
    {
        public string ProductDiscription { get; set; } = "";
        [Required]
        [Range(1, 1000000000, ErrorMessage = "Book Price must be between 1 to 100000")]
        public int ProductPrice { get; set; }
        [Required]
        public string ProductName { get; set; }

        public string ProductImage { get; set; }
        public bool InStock { get; set; }
        public bool InCart { get; set; }
        [Required]
        [Range(0, 1000000000, ErrorMessage = "Quantity must be between 0 to 10000000")]
        public int Quantity { get; set; }
    }
}
