using Library.Infrastructure;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Library.Controllers
{
    public class LoanController : Controller
    {
        private readonly AppDbContext _context;

        public LoanController(AppDbContext context)
        {
            _context = context;
        }

        //  Mostrar préstamos
        public IActionResult Index()
        {
            var loans = _context.Loan
                .Include(l => l.Book)
                .Include(l => l.User)
                .ToList();

            ViewBag.Users = _context.Users.ToList();
            ViewBag.Books = _context.Books.Where(b => b.AvailableCopies > 0).ToList();

            return View(loans);
        }

        //  Crear préstamo
        [HttpPost]
        public IActionResult Create(Loan loan)
        {
            var book = _context.Books.Find(loan.BookId);

            if (book == null)
                return NotFound();

            if (book.AvailableCopies <= 0)
            {
                TempData["Error"] = "No hay ejemplares disponibles para este libro.";
                return RedirectToAction(nameof(Index));
            }

            // Restar 1 ejemplar
            book.AvailableCopies--;
            book.Availability = book.AvailableCopies > 0;

            _context.Loan.Add(loan);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        //  Devolver préstamo
        [HttpPost]
        public IActionResult Return(int id)
        {
            var loan = _context.Loan
                .Include(l => l.Book)
                .FirstOrDefault(l => l.Id == id);

            if (loan == null)
                return NotFound();

            // Se asume que "ReturnDate" indica cuándo se devolvió
            loan.ReturnDate = DateTime.Now;
            loan.Book.AvailableCopies++;
            loan.Book.Availability = true;

            //  Registrar en historial
            var history = new History
            {
                UserId = loan.UserId,
                BookId = loan.BookId,
                DateHistory = DateTime.Now
            };

            _context.History.Add(history);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        //  Historial de usuario
        public IActionResult UserHistory(int userId)
        {
            var loans = _context.Loan
                .Include(l => l.Book)
                .Where(l => l.UserId == userId)
                .ToList();

            return View(loans);
        }

        //  Préstamos activos de un libro
        public IActionResult BookLoans(int bookId)
        {
            var loans = _context.Loan
                .Include(l => l.User)
                .Where(l => l.BookId == bookId)
                .ToList();

            return View(loans);
        }
    }
}
