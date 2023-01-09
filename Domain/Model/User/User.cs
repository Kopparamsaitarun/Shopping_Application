using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model.User
{
    public class User : BaseEntity
    {
        [Required(ErrorMessage = "Enter your First name")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Enter your Last name")]
        public string lastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", ErrorMessage = "Invalid pattern.")]
        public string email { get; set; }
        [Required(ErrorMessage = "Enter your Password"),DataType(DataType.Password)]
        public string password { get; set; }
        [NotMapped] // Does not effect with your database
        [Compare("password", ErrorMessage = "Password not matched"), Required(ErrorMessage = "Retype your password"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Enter your Phone number")]
        public string phoneNumber { get; set; }
        //[Required(ErrorMessage = "Accept the terms and conditions")]
        [Required(ErrorMessage = "Accept the terms and conditions"), Display(Name = "By enrolling your email, you consent to receive automated security notifications via email from ShoppingApp")]
        public bool policyFlag { get; set; }
        [Required(ErrorMessage = "Enter your Role")]
        public string Role { get; set; }
    }
}
