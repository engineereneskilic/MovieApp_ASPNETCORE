using System.ComponentModel.DataAnnotations;

namespace MovieApp.Web.Models
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
