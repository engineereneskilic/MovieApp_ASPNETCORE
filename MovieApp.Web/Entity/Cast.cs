using System.ComponentModel.DataAnnotations;

namespace MovieApp.Web.Entity
{
    public class Cast
    {
        [Key]
        public int CastID { get; set; }

        public Movie Movie { get; set; }
        public int MovieID { get; set; }

        public Person Person { get; set; }
        public int PersonID { get; set; }

        public string Name { get; set; } // o filmde aldığı isim
        public string Character { get; set; }// filmde hangi rolu aldı
    }
}
