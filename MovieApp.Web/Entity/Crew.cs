using System.ComponentModel.DataAnnotations;

namespace MovieApp.Web.Entity
{
    public class Crew // Yönetmen
    {
        [Key]
        public int CrewID { get; set; }

        public Movie Movie { get; set; }
        public int MovieID { get; set; }


        public Person Person { get; set; }
        public int PersonID { get; set; }

        public string Job { get; set; } /// meslekler var
    }
}
