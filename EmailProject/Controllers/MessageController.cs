using EmailProject.Context;
using EmailProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // =========================
        // 📩 YENİ MESAJ
        // =========================

        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> CreateMessage(Message message)
        {
            var user = await _userManager.GetUserAsync(User);

            var receiver = await _userManager.FindByEmailAsync(message.ReceiverEmail);

            if (receiver == null)
            {
                ModelState.AddModelError("", "Alıcı bulunamadı.");
                return View(message);
            }

            message.SenderEmail = user.Email;
            message.SentDate = DateTime.Now;
            message.IsStatus = false;

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Inbox));
        }


        // =========================
        // 📥 GELEN KUTUSU + ARAMA
        // =========================

        public async Task<IActionResult> Inbox(string search, bool starred = false)
        {
            var user = await _userManager.GetUserAsync(User);
           

            ViewBag.UserName = user.name;       // veya FirstName
            ViewBag.UserSurname = user.Surname; // veya LastName
            ViewBag.UserEmail = user.Email;
            ViewBag.UserImage = user.ImageUrl;

            ViewBag.UserEmail = user.Email;
            ViewBag.Search = search;
            ViewBag.IsStarred = starred;
            

            await LoadCounts(user);
            var query = _context.Messages
                .Where(x => x.ReceiverEmail == user.Email);

            // ⭐ Yıldız filtresi
            if (starred)
            {
                query = query.Where(x => x.IsStarred);
            }

            // 🔎 Arama filtresi
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x =>
                    x.Subject.Contains(search) ||
                    x.MessageDetail.Contains(search) ||
                    x.SenderEmail.Contains(search));
            }

            var values = await query
                .OrderByDescending(x => x.SentDate)
                .ToListAsync();

            // 🔥 DİNAMİK SAYAÇLAR
            ViewBag.InboxCount = await _context.Messages
                .CountAsync(x => x.ReceiverEmail == user.Email);

            ViewBag.StarredCount = await _context.Messages
                .CountAsync(x => x.ReceiverEmail == user.Email && x.IsStarred);

            ViewBag.SentCount = await _context.Messages
                .CountAsync(x => x.SenderEmail == user.Email);

            ViewBag.UnreadCount = await _context.Messages
                .CountAsync(x => x.ReceiverEmail == user.Email && !x.IsStatus);

            return View(values);
        }

        public async Task<IActionResult> SendBox(string search)
        {
            var user = await _userManager.GetUserAsync(User);

            ViewBag.UserEmail = user.Email;

            var query = _context.Messages
                .Where(x => x.SenderEmail == user.Email);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x =>
                    x.Subject.Contains(search) ||
                    x.MessageDetail.Contains(search) ||
                    x.ReceiverEmail.Contains(search));
            }
           
            var values = await query
                .OrderByDescending(x => x.SentDate)
                .ToListAsync();

            /// 🔥 DİNAMİK SAYAÇLAR
            ViewBag.InboxCount = await _context.Messages
                .CountAsync(x => x.ReceiverEmail == user.Email);

            ViewBag.StarredCount = await _context.Messages
                .CountAsync(x => x.ReceiverEmail == user.Email && x.IsStarred);

            ViewBag.SentCount = await _context.Messages
                .CountAsync(x => x.SenderEmail == user.Email);

            ViewBag.UnreadCount = await _context.Messages
                .CountAsync(x => x.ReceiverEmail == user.Email && !x.IsStatus);
            return View(values);
        }

        private async Task LoadCounts(AppUser user)
        {
            ViewBag.InboxCount = await _context.Messages
                .CountAsync(x => x.ReceiverEmail == user.Email);

            ViewBag.SentCount = await _context.Messages
                .CountAsync(x => x.SenderEmail == user.Email);
            ViewBag.UnreadCount = await _context.Messages
     .CountAsync(x => x.ReceiverEmail == user.Email && !x.IsStatus);

            ViewBag.StarredCount = await _context.Messages
                .CountAsync(x => x.ReceiverEmail == user.Email && x.IsStarred);
        }

        // =========================
        // 📖 MESAJ DETAY (OKUNDU YAPAR)
        // =========================

        public async Task<IActionResult> MessageDetail(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var value = await _context.Messages
                .FirstOrDefaultAsync(x =>
                    x.MessageId == id &&
                    (x.ReceiverEmail == user.Email || x.SenderEmail == user.Email));

            if (value == null)
                return NotFound();

            if (!value.IsStatus && value.ReceiverEmail == user.Email)
            {
                value.IsStatus = true;
                await _context.SaveChangesAsync();
            }

            return View(value);
        }


        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var value = await _context.Messages
                .FirstOrDefaultAsync(x =>
                    x.MessageId == id &&
                    (x.ReceiverEmail == user.Email || x.SenderEmail == user.Email));

            if (value == null)
                return NotFound();

            _context.Messages.Remove(value);
            await _context.SaveChangesAsync();
            // 🔥 DİNAMİK SAYAÇLAR
            ViewBag.InboxCount = await _context.Messages
                .CountAsync(x => x.ReceiverEmail == user.Email);

            ViewBag.StarredCount = await _context.Messages
                .CountAsync(x => x.ReceiverEmail == user.Email && x.IsStarred);

            ViewBag.SentCount = await _context.Messages
                .CountAsync(x => x.SenderEmail == user.Email);

            ViewBag.UnreadCount = await _context.Messages
                .CountAsync(x => x.ReceiverEmail == user.Email && !x.IsStatus);
            return RedirectToAction(nameof(Inbox));
        }
       
        [HttpPost]
        public async Task<IActionResult> ToggleStar(int id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
                return NotFound();

            message.IsStarred = !message.IsStarred;

            await _context.SaveChangesAsync();

            return Ok(message.IsStarred);
        }


        public async Task<IActionResult> Starred()
        {
            var user = await _userManager.GetUserAsync(User);

            var messages = await _context.Messages
                .Where(x => x.ReceiverEmail == user.Email && x.IsStarred)
                .ToListAsync();
            // 🔥 DİNAMİK SAYAÇLAR
            ViewBag.InboxCount = await _context.Messages
                .CountAsync(x => x.ReceiverEmail == user.Email);

            ViewBag.StarredCount = await _context.Messages
                .CountAsync(x => x.ReceiverEmail == user.Email && x.IsStarred);

            ViewBag.SentCount = await _context.Messages
                .CountAsync(x => x.SenderEmail == user.Email);

            ViewBag.UnreadCount = await _context.Messages
                .CountAsync(x => x.ReceiverEmail == user.Email && !x.IsStatus);
            return View(messages);
        }



        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var value = await _context.Messages.FindAsync(id);

            if (value != null)
            {
                value.IsStatus = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Inbox");
        }


        // =========================
        // 📤 GÖNDERİLEN KUTUSU
        // =========================

      
    }
}
