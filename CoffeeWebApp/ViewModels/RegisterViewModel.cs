using System.ComponentModel.DataAnnotations;

namespace CoffeeWebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Cần Nhập Email")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Cần Nhập Mật Khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Cần Xác Nhận Lại Mật Khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật Khẩu Nhập Lại Không Chính Xác")]
        public string ConfirmPassword { get; set; }
    }
}
