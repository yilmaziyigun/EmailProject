using EmailProject.Dtos;
using EmailProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EmailProject.Services;
using System;
using System.Threading.Tasks;

namespace EmailProject.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailService _emailService;

        public RegisterController(UserManager<AppUser> userManager, EmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        // ---------------- REGISTER ----------------
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            if (!dto.AcceptTerms)
            {
                ModelState.AddModelError("", "Kullanım şartlarını kabul etmelisiniz.");
                return View(dto);
            }

            var existingUser = await _userManager.FindByEmailAsync(dto.email);
            if (existingUser != null)
            {
                ViewBag.ShowActivateModal = true;
                ViewBag.Email = dto.email;
                return View(dto);
            }

            var user = new AppUser
            {
                UserName = dto.username,
                Email = dto.email,
                Surname = dto.surname,
                name = dto.name,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, dto.password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(dto);
            }

            // Confirm Code üret
            var code = new Random().Next(100000, 999999).ToString();
            user.ConfirmCode = code;
            await _userManager.UpdateAsync(user);

            // Mail gönder
            await _emailService.SendConfirmCodeAsync(user.Email, code);

            ViewBag.ShowActivateModal = true;
            ViewBag.Email = user.Email;

            return View(dto);
        }

        // ---------------- ACTIVATE USER ----------------
        [HttpPost]
        public async Task<IActionResult> ActivateUser(string Email, string Username, string ConfirmCode)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null || user.UserName != Username)
            {
                ViewBag.ActivateError = "Email veya kullanıcı adı yanlış!";
                ViewBag.ShowActivateModal = true;
                ViewBag.Email = Email;
                ViewBag.Username = Username;
                return View("CreateUser");
            }

            if (user.EmailConfirmed)
            {
                ViewBag.ActivateError = "Hesabınız zaten aktif.";
                ViewBag.ShowActivateModal = true;
                return View("CreateUser");
            }

            if (user.ConfirmCode != ConfirmCode)
            {
                ViewBag.ActivateError = "Kod yanlış!";
                ViewBag.ShowActivateModal = true;
                ViewBag.Email = Email;
                ViewBag.Username = Username;
                return View("CreateUser");
            }

            user.EmailConfirmed = true;
            user.ConfirmCode = null;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("UserLogin", "Login");
        }

    }
}
