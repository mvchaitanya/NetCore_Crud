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

namespace BookClub2.Controllers
{
    public class OpinionsController : Controller
    {
        private readonly BookClub2Context _context;

        public OpinionsController(BookClub2Context context)
        {
            _context = context;
        }

        // GET: Opinions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Opinion.Include(o => o.Book).ToListAsync());
        }

        // GET: Opinions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opinion = await _context.Opinion.Include(o => o.Book).SingleOrDefaultAsync(x => x.Id == id);
            if (opinion == null)
            {
                return NotFound();
            }

            return View(opinion);
        }

        // GET: Opinions/Create
        public IActionResult Create()
        {
            OpinionForm opinionForm = new OpinionForm();
            opinionForm.AllBooks = new List<Book>();
            foreach(Book book in _context.Book)
            {
                opinionForm.AllBooks.Add(book);
            }
            return View(opinionForm);
        }

        // POST: Opinions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Book_Id,Nick,Content,Grade")] OpinionForm opinionForm)
        {

            if (ModelState.IsValid)
            {
                Book book = _context.Book.Find(opinionForm.Book_Id);
                Opinion opinionToAdd = new Opinion(opinionForm.Nick, opinionForm.Content, opinionForm.Grade, book);
                _context.Add(opinionToAdd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            opinionForm.AllBooks = new List<Book>();
            foreach (Book book in _context.Book)
            {
                opinionForm.AllBooks.Add(book);
            }
            return View(opinionForm);
        }

        // GET: Opinions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opinion = await _context.Opinion.Include(o => o.Book).SingleOrDefaultAsync(x => x.Id == id);
            if (opinion == null)
            {
                return NotFound();
            }

            OpinionForm opinionForm = new OpinionForm(opinion);
            opinionForm.AllBooks = new List<Book>();
            foreach (Book book in _context.Book)
            {
                opinionForm.AllBooks.Add(book);
            }
            return View(opinionForm);
        }

        // POST: Opinions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Book_Id,Nick,Content,Grade")] OpinionForm opinionForm)
        {
            if (id != opinionForm.Id)
            {
                return NotFound();
            }
            Opinion opinionToAdd = null;
            if (ModelState.IsValid)
            {
                try
                {
                    Book book = _context.Book.Find(opinionForm.Book_Id);
                    opinionToAdd = new Opinion(opinionForm.Id, opinionForm.Nick, opinionForm.Content, opinionForm.Grade, book);
                    _context.Update(opinionToAdd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpinionExists(opinionToAdd.Id))
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
            opinionForm.AllBooks = new List<Book>();
            foreach (Book book in _context.Book)
            {
                opinionForm.AllBooks.Add(book);
            }
            return View(opinionForm);
        }

        // GET: Opinions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opinion = await _context.Opinion.Include(o => o.Book).SingleOrDefaultAsync(x => x.Id == id);
            if (opinion == null)
            {
                return NotFound();
            }

            return View(opinion);
        }

        // POST: Opinions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opinion = await _context.Opinion.FindAsync(id);
            _context.Opinion.Remove(opinion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpinionExists(int id)
        {
            return _context.Opinion.Any(e => e.Id == id);
        }
    }
}
