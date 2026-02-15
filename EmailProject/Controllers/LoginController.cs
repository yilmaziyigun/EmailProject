using EmailProject.Dtos;
using EmailProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmailProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginDto userLoginDto)
        {
            if (string.IsNullOrWhiteSpace(userLoginDto.username) ||
         string.IsNullOrWhiteSpace(userLoginDto.password))
            {
                ModelState.AddModelError("", "Kullanıcı adı ve şifre zorunludur.");
                return View(userLoginDto);
            }
            var user = await _signInManager.UserManager.FindByNameAsync(userLoginDto.username);

            if (user != null && !user.EmailConfirmed)
            {
                ModelState.AddModelError("", "Lütfen önce email doğrulaması yapın.");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(
                userLoginDto.username,
                userLoginDto.password,
                true,
                false);

            if (result.Succeeded)
            {
                return RedirectToAction("Inbox", "Message");
            }

            ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
            return View(userLoginDto);
        }

    }
}
