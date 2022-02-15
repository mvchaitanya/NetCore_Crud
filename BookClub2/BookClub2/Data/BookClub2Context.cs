#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookClub2.Models;

namespace BookClub2.Data
{
    public class BookClub2Context : DbContext
    {
        public BookClub2Context (DbContextOptions<BookClub2Context> options)
            : base(options)
        {
        }

        public DbSet<BookClub2.Models.Author> Author { get; set; }

        public DbSet<BookClub2.Models.Book> Book { get; set; }

        public DbSet<BookClub2.Models.Opinion> Opinion { get; set; }
    }
}
