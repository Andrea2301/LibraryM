using Library.Infrastructure;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Library.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (_context.Users.Any(u => u.Document == user.Document))
            {
                TempData["Error"] = "Ya existe un usuario con este documento.";
                return RedirectToAction(nameof(Index));
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            var existing = _context.Users.Find(user.Id);
            if (existing == null) return NotFound();

            if (_context.Users.Any(u => u.Document == user.Document && u.Id != user.Id))
            {
                TempData["Error"] = "Ya existe un usuario con este documento.";
                return RedirectToAction(nameof(Index));
            }

            existing.Name = user.Name;
            existing.Document = user.Document;
            existing.Email = user.Email;
            existing.Phone = user.Phone;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
