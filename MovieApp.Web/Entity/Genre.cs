using System.Collections.Generic;

namespace MovieApp.Web.Entity
{
    public class Genre
    {
        public int GenreID { get; set; }

        public string Name { get; set; }

        // ÖR: Macera türüne ait bütün filmlerin listesi
        public List<Movie> Movies { get; set; }
    }
}
