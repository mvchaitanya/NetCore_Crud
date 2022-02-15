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
    public class SearchController : Controller
    {
        private readonly BookClub2Context _context;

        public SearchController(BookClub2Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> SearchForm()
        {
            ViewBag.Books = await _context.Book.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ShowSearchResult(int BookId)
        {
            ViewBag.Books = await _context.Book.ToListAsync();
            ViewBag.Opinions = await _context.Opinion.Include(o => o.Book).Where(o => (o.Book.Id == BookId)).ToListAsync();
            return View("SearchForm");
        }

        public async Task<IActionResult> Rank()
        {
            List<Opinion> allOpinions = await _context.Opinion.Include(o => o.Book).ToListAsync();
            List<Book> allBooks = await _context.Book.ToListAsync();
            Dictionary<int, BookRank> BookRanks = new Dictionary<int, BookRank>();
            foreach(Book book in allBooks)
            {
                BookRanks.Add(book.Id, new BookRank(book));
            }
            foreach(Opinion opinion in allOpinions)
            {
                BookRanks[opinion.Book.Id].GradesNumber ++;
                BookRanks[opinion.Book.Id].GradeSum += opinion.Grade;
            }
            var list = BookRanks.Values.ToList();
            list.Sort();
            return View(list);
        }

        public async Task<IActionResult> Images()
        {
            return View();
        }

        public async Task<IActionResult> Links()
        {
            return View(await _context.Author.ToListAsync());
        }
    }
}
