using BookClub2.Models;
using System.ComponentModel.DataAnnotations;

namespace BookClub2.Controllers.Validators
{
    public class ExistingCountryAttribute : ValidationAttribute
    {

        private string[] validCountries = new string[] { "polska", "niemcy", "czechy", "anglia" };

        public string GetErrorMessage(string country) => $"Country: {country} does not exist";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var author = (Author)validationContext.ObjectInstance;
            var enteredCountry = ((string)value).ToLower();
            if (validCountries.Contains(enteredCountry))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(GetErrorMessage(enteredCountry));
        }

    }
}
