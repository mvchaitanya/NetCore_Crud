#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookClub2.Data;
using BookClub2.Models;
using Microsoft.Extensions.Logging;

namespace BookClub2.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookClub2Context _context;

        private readonly ILogger _logger;

        public BooksController(BookClub2Context context, ILogger<BooksController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Book.Include(a => a.Author).ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.Include(a => a.Author).SingleOrDefaultAsync(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            
            BookForm bookForm = new BookForm();
            bookForm.AllAuthors = new List<Author>();
            foreach(Author author in _context.Author)
            {
                bookForm.AllAuthors.Add(author);
            }

            return View(bookForm);
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author_ID,IssueDate,ISBN")] BookForm bookForm)
        {
            if (ModelState.IsValid)
            {
                Author author = _context.Author.Find(bookForm.Author_ID);
                Book bookToAdd = new Book(bookForm.Title, bookForm.IssueDate, bookForm.ISBN, author);
                _context.Add(bookToAdd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            bookForm.AllAuthors = new List<Author>();
            foreach (Author author in _context.Author)
            {
                bookForm.AllAuthors.Add(author);
            }
            return View(bookForm);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book book = await _context.Book.Include(a => a.Author).SingleOrDefaultAsync(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            BookForm bookForm = new BookForm(book);
            bookForm.Author_ID = book.Author.Id;
            bookForm.AllAuthors = new List<Author>();
            foreach (Author author in _context.Author)
            {
                bookForm.AllAuthors.Add(author);
            }

            return View(bookForm);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author_ID,IssueDate,ISBN")] BookForm bookForm)
        {
            if (id != bookForm.Id)
            {
                return NotFound();
            }

            Book bookToAdd = null;
            if (ModelState.IsValid)
            {
                try
                {
                    Author author = _context.Author.Find(bookForm.Author_ID);
                    bookToAdd = new Book(bookForm.Id, bookForm.Title, bookForm.IssueDate, bookForm.ISBN, author);
                    _context.Update(bookToAdd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(bookToAdd.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            bookForm.AllAuthors = new List<Author>();
            foreach (Author author in _context.Author)
            {
                bookForm.AllAuthors.Add(author);
            }
            return View(bookForm);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.Include(a => a.Author).SingleOrDefaultAsync(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
