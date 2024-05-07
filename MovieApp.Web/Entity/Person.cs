using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MovieApp.Web.Entity
{
    public class Person
    {
        [Key]
        public int personID { get; set; }

        public string Name { get; set; }

        public string Biography { get; set; }

        public string Imdb { get; set; }

        public string HomePage { get; set; }

        public string PlaceOfBirth { get; set; }

        public User User { get; set; }

        public int UserID { get; set; } //  foreign key, uniqe key  > ilgili person tablosunda sadece bir kere kullnaılmak zorunda bire bir ilişki

    }
}
