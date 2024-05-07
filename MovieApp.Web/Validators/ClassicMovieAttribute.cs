using Microsoft.VisualBasic;
using MovieApp.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Web.Validators
{
    public class ClassicMovieAttribute : ValidationAttribute
    {

        public ClassicMovieAttribute(int year)
        {
            Year = year;
        }

        public int Year { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var movie = (AdminCreateMovieModel)validationContext.ObjectInstance;
            var releaseYear = ((DateTime)value).Year;

            if (movie.isClassic && releaseYear < Year) //1950 yılı ve öncesi bir değer girmelisiniz 
            {
                return new ValidationResult($"Klasik filmler için {Year} ve öncesi değer girmelisiniz");
            }

            return ValidationResult.Success;
        }
    }
}
