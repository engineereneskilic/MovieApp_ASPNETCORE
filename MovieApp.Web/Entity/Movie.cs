using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Web.Entity
{
    public class Movie
    {
        // microsft data annotations
        // Primary Key => Id, <TypeName>Id

        public Movie()
        {
            Genres = new List<Genre>();
        }

        public int MovieID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }


       // public DbSet<Director> Directors { get; set; }
        //public string Director { get; set; }

        public string ImageUrl { get; set; }

        public List<Genre> Genres { get; set; }
       // public Genre Genre { get; set; }
    }
}
