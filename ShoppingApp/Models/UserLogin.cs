using Domain;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models
{
    public class UserLogin : BaseEntity
    {
        [Required]
        public string EmailId { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
