using BTL1.Data;
using BTL1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL1.Controllers
{
    public class AdminController : Controller
    {
        private readonly BTLDbContext _context;

        public AdminController(BTLDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users); 
        }

        // Danh sách User
        public IActionResult ManageUsers()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // Chỉnh sửa User
        public IActionResult EditUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(User updatedUser)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Update(updatedUser);
                _context.SaveChanges();
                return RedirectToAction("ManageUsers");
            }
            return View(updatedUser);
        }

        // Xóa User
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("ManageUsers");
        }

        // Danh sách Jobs
        public IActionResult ManageJobs()
        {
            var jobs = _context.Jobs.ToList();
            return View(jobs);
        }

        // Chỉnh sửa Job
        public IActionResult EditJob(int id)
        {
            var job = _context.Jobs.Find(id);
            if (job == null) return NotFound();
            return View(job);
        }

        [HttpPost]
        public IActionResult EditJob(Job updatedJob)
        {
            if (ModelState.IsValid)
            {
                _context.Jobs.Update(updatedJob);
                _context.SaveChanges();
                return RedirectToAction("ManageJobs");
            }
            return View(updatedJob);
        }

        // Xóa Job
        public IActionResult DeleteJob(int id)
        {
            var job = _context.Jobs.Find(id);
            if (job == null) return NotFound();

            _context.Jobs.Remove(job);
            _context.SaveChanges();
            return RedirectToAction("ManageJobs");
        }
    }
}
