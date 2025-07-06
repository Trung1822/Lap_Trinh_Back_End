using BTL1.Data;
using BTL1.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL1.Controllers
{
    public class RecruitmentController : Controller
    {
        private readonly BTLDbContext _context;

        public RecruitmentController(BTLDbContext context)
        {
            _context = context;
        }

        // Hiển thị form đăng bài tuyển dụng
        public IActionResult Create()
        {
            // Chỉ cho người đăng nhập mới được vào
            if (HttpContext.Session.GetInt32("UserID") == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        // Xử lý đăng bài
        [HttpPost]
        public IActionResult Create(Job job)
        {
            if (ModelState.IsValid)
            {
                job.PostedDate = DateTime.Now;
                _context.Jobs.Add(job);
                _context.SaveChanges();

                return RedirectToAction("Index", "Jobs");
            }

            return View(job);
        }
    }
}
