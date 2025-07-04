using BTL1.Data;
using BTL1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BTL1.Controllers
{
    public class AccountController : Controller
    {
        private readonly BTLDbContext _context;

        public AccountController(BTLDbContext context)
        {
            _context = context;
        }

        // Hiển thị form đăng ký
        public IActionResult Register() => View();

        // Xử lý đăng ký
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // Hiển thị form đăng nhập
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                // Debug xem database đang trỏ về đâu
                var connStr = _context.Database.GetConnectionString();
                Console.WriteLine($"Current Connection String: {connStr}");
            }

            if (user != null)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                HttpContext.Session.SetString("FullName", user.FullName);
#pragma warning restore CS8604 // Possible null reference argument.
                HttpContext.Session.SetInt32("UserID", user.UserID);
                return RedirectToAction("Index", "Jobs");
            }

            ViewBag.Error = "Sai thông tin đăng nhập";
            return View();
        }



        // Đăng xuất
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Jobs");
        }

        // Hiển thị profile người dùng
        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
                return RedirectToAction("Login");

            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);
            if (user == null)
                return NotFound();

            return View(user);
        }
    }
}
