using System.ComponentModel.DataAnnotations;

namespace BookClub2.Models
{
    public class Opinion
    {
        public int Id { get; set; }
        [StringLength(16, ErrorMessage = "Length of nick must be lower than 16")]
        public string? Nick { get; set; }

        [StringLength(64, ErrorMessage = "Length of content must be lower than 64")]
        public string? Content { get; set; }

        [Range(1,6, ErrorMessage = "Grade must be between 1-6")]
        public int Grade { get; set; }

        [Required(ErrorMessage = "Please add book")]
        public Book Book { get; set; }

        public Opinion()
        {

        }
        public Opinion(string nick, string content, int grade, Book book)
        {
            Nick = nick;
            Content = content;
            Grade = grade;
            Book = book;
        }

        public Opinion(int id, string nick, string content, int grade, Book book)
        {
            Id = id;
            Nick = nick;
            Content = content;
            Grade = grade;
            Book = book;
        }
    }
}
