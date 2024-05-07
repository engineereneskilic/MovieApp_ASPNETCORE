using System.Collections.Generic;

namespace MovieApp.Web.Entity
{
    public class EditPageViewModel
    {
        public Movie Movie { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
