using EmailProject.Context;
using EmailProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmailProject.Controllers
{

    public class MessageController : Controller
    {
        private readonly EmailContext _context;
        private readonly UserManager<AppUser> _userManager;

        public MessageController(EmailContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            message.SentDate = DateTime.Now;
            message.IsStatus = false;
            _context.Messages.Add(message);
            _context.SaveChanges();
            return RedirectToAction("SendBox");
        }
        public async Task<IActionResult> Inbox()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _context.Messages.Where(x => x.ReceiverEmail == user.Email).ToList();
            return View(values);
        }    
    }
}
