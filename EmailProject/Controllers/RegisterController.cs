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

            // EMAIL KONTROL
            var emailUser = await _userManager.FindByEmailAsync(dto.email);
            if (emailUser != null)
            {
                if (!emailUser.EmailConfirmed)
                {
                    return RedirectToAction("ActivateUser", new
                    {
                        email = emailUser.Email,
                        username = emailUser.UserName
                    });
                }

                ModelState.AddModelError("", "Bu email zaten kayıtlı ve aktif.");
                return View(dto);
            }


            // USERNAME KONTROL
            var usernameUser = await _userManager.FindByNameAsync(dto.username);
            if (usernameUser != null)
            {
                ModelState.AddModelError("", "Bu kullanıcı adı zaten alınmış.");
                return View(dto);
            }

            // KULLANICI OLUŞTUR
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

            // CONFIRM CODE
            var code = new Random().Next(100000, 999999).ToString();
            user.ConfirmCode = code;
            await _userManager.UpdateAsync(user);

            //email gönder
            await _emailService.SendConfirmCodeAsync(user.Email, code);

            // MODAL AÇ
            return RedirectToAction("ActivateUser", new
            {
                email = user.Email,
                username = user.UserName
            });

        }


        // ---------------- ACTIVATE USER ----------------
        [HttpGet]
        public IActionResult ActivateUser(string email, string username)
        {
            ViewBag.Email = email;
            ViewBag.Username = username;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ActivateUser(string Email, string Username, string ConfirmCode)
        {
            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null || user.UserName != Username)
            {
                TempData["ActivateError"] = "Email veya kullanıcı adı yanlış!";
                return RedirectToAction("ActivateUser", new { email = Email, username = Username });
            }

            if (user.EmailConfirmed)
            {
                TempData["ActivateError"] = "Hesabınız zaten aktif.";
                return RedirectToAction("ActivateUser", new { email = Email, username = Username });
            }

            if (user.ConfirmCode != ConfirmCode)
            {
                TempData["ActivateError"] = "Kod yanlış!";
                return RedirectToAction("ActivateUser", new { email = Email, username = Username });
            }

            user.EmailConfirmed = true;
            user.ConfirmCode = null;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("UserLogin", "Login");
        }


    }
}
