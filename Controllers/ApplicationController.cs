using BTL1.Data;
using BTL1.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL1.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly BTLDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ApplicationController(BTLDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost]
        public IActionResult Submit(int jobId, IFormFile resumeFile)
        {
            if (resumeFile == null || resumeFile.Length == 0)
            {
                TempData["Error"] = "Vui lòng chọn file CV.";
                return RedirectToAction("Details", "Jobs", new { id = jobId });
            }

            // Lưu file vào wwwroot/uploads
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(resumeFile.FileName);
            var filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                resumeFile.CopyTo(stream);
            }

            // Lưu vào database
            var application = new Application
            {
                CandidateID = HttpContext.Session.GetInt32("UserID") ?? 0,
                JobID = jobId,
                ResumePath = fileName,
                AppliedDate = DateTime.Now
            };

            _context.Applications.Add(application);
            _context.SaveChanges();

            TempData["Success"] = "Nộp CV thành công!";
            return RedirectToAction("Details", "Jobs", new { id = jobId });
        }

        // Trang admin xem danh sách CV
        public IActionResult Manage()
        {
            var applications = _context.Applications.ToList();
            return View(applications);
        }
    }
}
