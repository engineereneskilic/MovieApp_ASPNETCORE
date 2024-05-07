using System.Collections.Generic;
using System.Linq;
using MovieApp.Web.Entity;

namespace MovieApp.Web.Data
{
    public class GenreRepository
    {
        private static readonly List<Genre> _genres = null;

        static GenreRepository()
        {
            _genres = new List<Genre>()
            {
                new Genre{GenreID = 1,Name = "Macera"},
                new Genre{GenreID = 2,Name = "Komedi"},
                new Genre{GenreID = 3,Name = "Romantik"},
                new Genre{GenreID = 4,Name = "Savaş"}
            };
        }

        public static List<Genre> Genres
        {
            get
            {
                return _genres;
            }
        }

        public static void Add(Genre genre)
        {
            _genres.Add(genre);
        }

        public static void Remove(Genre genre)
        {
            _genres.Remove(genre);
        }

        public static Genre GetByID(int id)
        {
            return _genres.FirstOrDefault(x => x.GenreID == id);
        }
     }
}
