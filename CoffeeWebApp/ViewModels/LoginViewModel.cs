using System.ComponentModel.DataAnnotations;

namespace CoffeeWebApp.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name ="Email Address")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
