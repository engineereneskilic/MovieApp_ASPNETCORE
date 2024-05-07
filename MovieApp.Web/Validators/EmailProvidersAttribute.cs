using System.ComponentModel.DataAnnotations;

namespace MovieApp.Web.Validators
{
    public class EmailProvidersAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string email = "";

            if(value != null)
            {
                email = value.ToString();
            }
            else
            {
                return new ValidationResult("Hatalı eposta sunucusu");
            }
            return new ValidationResult("Hatalı eposta sunucusu");

        }
    }
}
