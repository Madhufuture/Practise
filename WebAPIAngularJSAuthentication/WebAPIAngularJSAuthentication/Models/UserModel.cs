using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAPIAngularJSAuthentication.Models
{
    public class UserModel
    {
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100,ErrorMessage = "The {0} must be at least {2} characters long",MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        [Compare("Password",ErrorMessage = "Password and confirmation password doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}