using Microsoft.AspNetCore.Mvc;
using BTL1.Data;
using BTL1.Models;
using System.Linq;

namespace BTL1.Controllers
{
    public class JobsController : Controller
    {
        private readonly BTLDbContext _context;

        public JobsController(BTLDbContext context)
        {
            _context = context;
        }

        // Trang danh sách việc làm + tìm kiếm
        public IActionResult Index(string? keyword, string? location)
        {
            var jobs = _context?.Jobs.AsQueryable();

            if (jobs == null)
            {
                return NotFound(); // Handle null context or Jobs collection
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                jobs = jobs.Where(j => j.Title != null && j.Title.Contains(keyword));
            }
            if (!string.IsNullOrEmpty(location))
            {
                jobs = jobs.Where(j => j.Location != null && j.Location.Contains(location));
            }

            return View(jobs.OrderByDescending(j => j.PostedDate).ToList());
        }

        // Trang chi tiết công việc
        public IActionResult Details(int id)
        {
            var job = _context?.Jobs.FirstOrDefault(j => j.JobID == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }
    }
}
