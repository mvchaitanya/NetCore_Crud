using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookClub2.Models
{
    public class BookForm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter book title")]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "Length of title must between 1-32")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter issue date")]
        [Range(1, 2022, ErrorMessage = "Year must be between 1-2022")]
        public int IssueDate { get; set; }

        [RegularExpression(@"^[0-9]{10,13}$", ErrorMessage = "Enter valid ISBN number")]
        public string ISBN { get; set; }

        [Required]
        public int Author_ID { get; set; }

        [ValidateNever]
        public List<Author> AllAuthors { get; set; }

        public BookForm()
        {
        }

        public BookForm(string title, int issueDate, string iSBN, List<Author> allAuthors)
        {
            Title = title;
            IssueDate = issueDate;
            ISBN = iSBN;
            AllAuthors = allAuthors;
        }

        public BookForm(Book book)
        {
            Id = book.Id;
            Title = book.Title;
            IssueDate = book.IssueDate;
            ISBN = book.ISBN;
        }
    }
}
