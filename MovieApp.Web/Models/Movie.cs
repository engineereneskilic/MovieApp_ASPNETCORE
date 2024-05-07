using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Web.Models
{
    public class Movie
    {
        // microsft data annotations
        public int MovieID { get; set; }

        [DisplayName("Başlık")]
        [Required(ErrorMessage = "Film Başlığı eklemelisiniz")]
        [StringLength(50, MinimumLength = 5,ErrorMessage = "Film Başlığı 5-10 karakter aralığında olmalıdır.")]
        public string Title { get; set; }

        [DisplayName("Açıklama")]
        [Required]
        public string Description { get; set; }

        [DisplayName("Yönetmen")]
        [Required]
        public string Director { get; set; }


        [DisplayName("Oyuncular")]
        public string[] Players { get; set; }


        public string ImageUrl { get; set; }


        [Required]
        public Genre Genre { get; set; }  // navigation property

        public int GenreID { get; set; }
        
    }
}
