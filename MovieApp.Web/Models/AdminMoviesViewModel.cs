using MovieApp.Web.Entity;
using MovieApp.Web.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MovieApp.Web.Models
{
    public class AdminMoviesViewModel
    {
        public List<AdminMovieViewModel> Movies { get; set; }
    }

    public class AdminMovieViewModel
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public List<Entity.Genre> Genres  { get; set; }
    }

    public class AdminCreateMovieModel
    {
        [Display(Name = "Film Adı")]
        [Required(ErrorMessage = "Film Adı Girmelisiniz.")]
        [StringLength(50,MinimumLength = 3, ErrorMessage = "Film Adı için 3-50 karakter girmelisiniz")]
        public string Title { get; set; }


        [Display(Name = "Film Açıklama")]
        [Required(ErrorMessage = "Film Açıklaması Girmelisiniz.")]
        [StringLength(3000, MinimumLength = 10, ErrorMessage = "Film Açıklaması için 10-3000 karakter girmelisiniz")]
        public string Description { get; set; }


        [Required(ErrorMessage = "En az bir tür seçmelisiniz")]
        public int[] GenreIds { get; set; }


        public bool isClassic { get; set; }


        [ClassicMovie(1950)]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

    }

    public class AdminEditViewModel
    {
        public int MovieID { get; set; }

        [Display(Name = "Film Adı")]
        [Required(ErrorMessage = "Film Adı Girmelisiniz.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Film Adı için 3-50 karakter girmelisiniz")]
        public string Title { get; set; }


        [Display(Name = "Film Açıklama")]
        [Required(ErrorMessage = "Film Açıklaması Girmelisiniz.")]
        [StringLength(3000, MinimumLength = 10, ErrorMessage = "Film Açıklaması için 10-3000 karakter girmelisiniz")]
        public string Description { get; set; }


        public string ImageURL { get; set; }
        //public List<Entity.Genre> SelectedGenres { get; set; }

        [Required(ErrorMessage = "Lütfen en az bir tür bilgisi seçiniz.")]
        public int[] GenreIds { get; set; }

    }
}
