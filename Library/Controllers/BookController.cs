using Library.Infrastructure; //  para AppDbContext
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq; //necesario para LINQ

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        // Mostrar lista de libros
        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        // Editar libro (POST)
        [HttpPost]
        public IActionResult Edit(Book updatedBook)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == updatedBook.Id);
            if (book == null)
                return NotFound();

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Availability = updatedBook.Availability; //  propiedad correcta

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
