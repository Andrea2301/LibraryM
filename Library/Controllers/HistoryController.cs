using Library.Infrastructure;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Library.Controllers
{
    public class HistoryController : Controller
    {
        private readonly AppDbContext _context;

        public HistoryController(AppDbContext context)
        {
            _context = context;
        }

        //  Mostrar historial completo
        public IActionResult Index()
        {
            var history = _context.History
                .Include(h => h.User)
                .Include(h => h.Book)
                .OrderByDescending(h => h.DateHistory)
                .ToList();

            return View(history);
        }

        // Historial por usuario
        public IActionResult ByUser(int userId)
        {
            var history = _context.History
                .Include(h => h.Book)
                .Where(h => h.UserId == userId)
                .OrderByDescending(h => h.DateHistory)
                .ToList();

            ViewBag.User = _context.Users.Find(userId);
            return View("Index", history);
        }

        //  Historial por libro
        public IActionResult ByBook(int bookId)
        {
            var history = _context.History
                .Include(h => h.User)
                .Where(h => h.BookId == bookId)
                .OrderByDescending(h => h.DateHistory)
                .ToList();

            ViewBag.Book = _context.Books.Find(bookId);
            return View("Index", history);
        }
    }
}
