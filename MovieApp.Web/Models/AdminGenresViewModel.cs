﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Web.Models
{
    public class AdminGenresViewModel
    {
        [Required(ErrorMessage = "Tür bilgisi girmelisiniz.")]
        public string Name { get; set; }

        public List<AdminGenreViewModel> Genres { get; set; }
    }

    public class AdminGenreViewModel
    {
        public int GenreID { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

    }

    public class AdminGenreEditViewModel
    {
        public int GenreID { get; set; }


        [Required(ErrorMessage = "Tür bilgisi girmelisiniz.")]
        public string Name { get; set; }

        public List<AdminMovieViewModel> Movies { get; set; }
        // Movies[0].MovieID, Movies[0].Name, Movies[1]

        // Movies[1].MovieID, Movies[0].Name, Movies[1]
        // Movies[0].MovieID, Movies[0].Name, Movies[1]
    }
}
