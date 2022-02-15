using System.ComponentModel.DataAnnotations;

namespace BookClub2.Models
{
    public class Book
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

        [Required(ErrorMessage = "Please select author")]
        public Author Author { get; set; }

        public Book()
        {

        }

        public Book(string title, int issueDate, string iSBN, Author author)
        {
            Title = title;
            IssueDate = issueDate;
            ISBN = iSBN;
            Author = author;
        }

        public Book(int id, string title, int issueDate, string iSBN, Author author)
        {
            Id = id;
            Title = title;
            IssueDate = issueDate;
            ISBN = iSBN;
            Author = author;
        }
    }
}
