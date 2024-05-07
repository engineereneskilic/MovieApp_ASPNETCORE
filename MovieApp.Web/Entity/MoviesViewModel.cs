
using System.Collections.Generic;
using System.Linq;

namespace MovieApp.Web.Entity
{
    public class MoviesViewModel
    {
        public IQueryable<Movie> Movies {get; set; }
        public List<Genre> Genres { get; set; }
    }
}
