using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShoppingApp.Models
{
    public class User 
    {
        [Key]
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [Display(Name = "")]
        [Required]
        public bool policyFlag { get; set; }
    }
}
