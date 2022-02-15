using System.ComponentModel.DataAnnotations;
using BookClub2.Controllers.Validators;

namespace BookClub2.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter author forename")]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "Length of forename must between 1-32")]
        public string Forename { get; set; }

        [Required(ErrorMessage = "Please enter author surname")]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "Length of forename must between 1-32")]
        public string Surname { get; set; }

        [Range(0, 2022, ErrorMessage = "Yov 0-2022 only")]
        public int Yob { get; set; }

        [Required(ErrorMessage = "Please enter author country of birth")]
        [ExistingCountry]
        public string CountryOfBirth { get; set; }

        public string fullname => Forename + " " + Surname;

        public Author()
        {

        }

        public Author(int id, string forename, string surname, int yob, string countryOfBirth)
        {
            Id = id;
            Forename = forename;
            Surname = surname;
            Yob = yob;
            CountryOfBirth = countryOfBirth;
        }
    }
}
