using System;
using System.ComponentModel.DataAnnotations;
namespace Domain.Model.Dashboard
{
    public class Productlist : BaseEntity
    {
        public string ProductDescription { get; set; } = "";
        [Required]
        [Range(1, 100000, ErrorMessage = "Product Price must be between 1 to 100000")]
        public int ProductPrice { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public bool InStock { get; set; }
        public bool InCart { get; set; }
        [Required]
        [Range(0, 100000, ErrorMessage = "Quantity must be between 0 to 100000")]
        public int Quantity { get; set; }
    }
}
