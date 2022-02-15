using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookClub2.Models
{
    public class OpinionForm : IValidatableObject
    {
        public int Id { get; set; }
        public string? Nick { get; set; }

        public string? Content { get; set; }

        [Range(1, 6, ErrorMessage = "Grade must be between 1-6")]
        public int Grade { get; set; }

        [Required]
        public int Book_Id { get; set; }

        [ValidateNever]
        public List<Book> AllBooks { get; set; }

        public OpinionForm()
        {
        }

        public OpinionForm(string nick, string content, int grade, List<Book> allBooks)
        {
            Nick = nick;
            Content = content;
            Grade = grade;
            AllBooks = allBooks;
        }

        public OpinionForm(Opinion opinion)
        {
            Id = opinion.Id;
            Nick = opinion.Nick;
            Content = opinion.Content;
            Grade = opinion.Grade;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var opinionForm = (OpinionForm)validationContext.ObjectInstance;
            if(String.IsNullOrEmpty(Nick)) {
                if(String.IsNullOrEmpty(Content))
                {
                    yield return new ValidationResult("You must enter nick or content", new string[] { "Nick & Content" });
                }
                else
                {
                    if(Content.Length > 64)
                    {
                        yield return new ValidationResult("Content length must be lower than 64", new string[] { "Nick & Content" });

                    }
                }
            }
            else
            {
                if(Nick.Length > 16)
                {
                    yield return new ValidationResult("Nick length must be lower than 16", new string[] { "Nick & Content" });
                }
                if(!String.IsNullOrEmpty(Content))
                {
                    if(Content.Length > 64)
                    {
                        yield return new ValidationResult("Content length must be lower than 64", new string[] { "Nick & Content" });
                    }
                }
            }

        }
    }
}
