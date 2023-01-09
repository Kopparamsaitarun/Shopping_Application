using Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace ShoppingApp.Models
{
    public class User : BaseEntity
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string ConfirmPassword { get; set; }
        public string phoneNumber { get; set; }
        public bool policyFlag { get; set; }
        public string Role { get; set; }
    }
}
