using EmailProject.Dtos;
using EmailProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmailProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditDto userEditDto = new UserEditDto();

            userEditDto.Username = user.UserName;
            userEditDto.Name = user.name;
            userEditDto.Surname = user.Surname;
            userEditDto.ImageUrl = user.ImageUrl;
            userEditDto.Email = user.Email;
            
            return View(userEditDto);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserEditDto userEditDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.name = userEditDto.Name;
            user.Surname = userEditDto.Surname;
            user.UserName = userEditDto.Username;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userEditDto.Password);
            user.Email = userEditDto.Email;
            var resource = Directory.GetCurrentDirectory();
            var extension = Path.GetExtension(userEditDto.Image.FileName);
            var newImageName = Guid.NewGuid() + extension;
            var saveLocation = resource+ "/wwwroot/images/" + newImageName;
            var stream = new FileStream(saveLocation, FileMode.Create);
            await userEditDto.Image.CopyToAsync(stream);
            user.ImageUrl =  newImageName;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Inbox", "Message");
            }
            return View();
        }
    }
}
