using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Components.Forms;
using MovieApp.Web.Entity;

namespace MovieApp.Web.Data
{
    public class MovieRepository
    {
        private static readonly List<Movie> _movies = null;

        static MovieRepository() // iptal
        {
            //_movies = new List<Movie>
            //{
                //new Movie
                //{

                //    Title = "Film 1", Description = "Film 1 Açıklaması", /*Director = "Film Director",*/
                //    //Players = new string[] { "Oyuncu 1", "Oyuncu 2", "Oyuncu 3" },
                //    ImageUrl = "1.jpg",
                //},
                //new Movie
                //{
                //    MovieID = 2,
                //    Title = "Film 2", Description = "Film 2 Açıklaması", /*Director = "Film 2 Director",*/
                //   // Players = new string[] { "Oyuncu 11", "Oyuncu 22", "Oyuncu 33" },
                //    ImageUrl = "2.jpg",
                //    GenreID = 4
                //},
                //new Movie
                //{
                //    MovieID = 3,
                //    Title = "Film 3", Description = "Film 3 Açıklaması", /*Director = "Film 3 Açıklaması",*/
                //   // Players = new string[] { "Oyuncu 44", "Oyuncu 55", "Oyuncu 66" },
                //    ImageUrl = "3.jpg",
                //    GenreID = 4
                    
                //},
                //new Movie
                //{
                //    MovieID = 4,
                //    Title = "Film 4", Description = "Film 4 Açıklaması", /*Director = "Film Director",*/
                //    //Players = new string[] { "Oyuncu 1", "Oyuncu 2", "Oyuncu 3" },
                //    ImageUrl = "4.webp",
                //    GenreID = 1
                //},
                //new Movie
                //{
                //    MovieID = 5,
                //    Title = "Film 5", Description = "Film 5 Açıklaması", /*Director = "Film 2 Director",*/
                //   // Players = new string[] { "Oyuncu 11", "Oyuncu 22", "Oyuncu 33" },
                //    ImageUrl = "5.webp",
                //    GenreID = 3
                //},
                //new Movie
                //{
                //    MovieID = 6,
                //    Title = "Film 6", Description = "Film 6 Açıklaması", /*Director = "Film 3 Açıklaması",*/
                //   // Players = new string[] { "Oyuncu 44", "Oyuncu 55", "Oyuncu 66" },
                //    ImageUrl = "6.webp",
                //    GenreID = 1
                //}
            //};

        }

        public static List<Movie> Movies
        {
            get
            {
                return _movies;
            }
        }

        public static void Add(Movie movie)
        {
            movie.MovieID = _movies.Count + 1;

            _movies.Add(movie);
        }


        private static void Remove(Movie movie)
        {
            _movies.Remove(movie);
        }

        public static Movie GetByID(int id)
        {
            return _movies.FirstOrDefault(x => x.MovieID == id);
        }

        public static void Edit(Movie m)
        {
            foreach (var movie in _movies)
            {
                if (movie.MovieID == m.MovieID)
                {
                    movie.Title = m.Title;
                    movie.Description = m.Description;
                    //movie.Director = m.Director;
                    movie.ImageUrl = m.ImageUrl;
                    //movie.GenreID = m.GenreID;
                    break;
                }
            }
        }

        public static void Delete(int MovieId)
        {
            var movie = GetByID(MovieId);
            if (movie != null)
            {
                _movies.Remove(movie);
            }
        }

    }
}
