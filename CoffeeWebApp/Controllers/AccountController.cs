using CoffeeWebApp.Data;
using CoffeeWebApp.Models;
using CoffeeWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;

        public AccountController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager; 
            _signInManager = signInManager;
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            var respone = new LoginViewModel();
            return View(respone);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user != null)
            {
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    //Password correct, sign in
                    user.AccessFailedCount = 0;
                    await _userManager.UpdateAsync(user);

                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        if (User.IsInRole("admin"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
                //Password is incorrect
                var count = user.AccessFailedCount += 1;
                await _userManager.UpdateAsync(user);
                TempData["messageFailLoginCount"] = $"Đăng nhập thất bại lần {count}";
                return View(loginViewModel);
            }
            //User not found
            TempData["Error"] = "Lỗi Đăng Nhập!";
            return View(loginViewModel);
        }
        [HttpGet]
        public IActionResult Register()
        {
            var respone = new RegisterViewModel();
            return View(respone);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "Email này đã có người sử dụng";
                return View(registerViewModel);
            }

            var newUser = new UserModel()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress,
                EmailConfirmed = true,
                
            };

            var newUserRespone = await _userManager.CreateAsync(newUser,registerViewModel.Password);
 
            if (newUserRespone.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            else
            {
                TempData["Error"] = "Mật Khẩu Gồm 8 Kí Tự Trở Lên";
                return View(registerViewModel);
            }
            TempData["Success"] = "Tạo Tài Khoản Thành Công Đang Chuyển Hướng Đến Trang Đăng Nhập";
            return View(registerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

    }
}
