using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Web.Entity;

namespace MovieApp.Web.Data
{
    public static class DataSeeding
    {
        public static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<MovieContext>();

            // veritabanına güncellemeleri gönder
            // dotnet ef database update
            context.Database.Migrate();

            var genres = new List<Genre>()
            {
                new Genre
                {
                    Name = "Macera", Movies = 
                                        new List<Movie>()
                                        {
                                            new Movie
                                            {
                                                Title = "Macera 1",
                                                Description = "Macera 1 açıklaması",
                                                ImageUrl = "1.jpg"
                                                
                                            },
                                            new Movie
                                            {

                                                Title = "Macera 2",
                                                Description = "Macera 2 açıklaması",
                                                ImageUrl = "2.jpg"

                                            }
                                        }
                },
                new Genre
                {
                    Name = "Komedi", Movies =
                        new List<Movie>()
                        {
                            new Movie
                            {
                                Title = "Komedi 1",
                                Description = "Komedi 1 açıklaması",
                                ImageUrl = "3.jpg"

                            },
                            new Movie
                            {

                                Title = "Komedi 2",
                                Description = "Komedi 2 açıklaması",
                                ImageUrl = "4.jpg"

                            }
                        }
                },
                new Genre { Name = "Romantik" },
                new Genre { Name = "Savaş" }
            };

            var movies = new List<Movie>()
            {

                new Movie
                {
                    //MovieID = 1,
                    Title = "Film 1", Description = "Film 1 Açıklaması", /*Director = "Film Director",*/
                    //Players = new string[] { "Oyuncu 1", "Oyuncu 2", "Oyuncu 3" },
                    ImageUrl = "1.jpg",
                    Genres = new List<Genre>()
                    {
                        genres[1],
                        genres[2],
                        genres[3]
                    }
                },
                new Movie
                {
                    // MovieID = 2,
                    Title = "Film 2",
                    Description = "Film 2 Açıklaması", /* Director = "Film 2 Director",*/
                    // Players = new string[] { "Oyuncu 11", "Oyuncu 22", "Oyuncu 33" },
                    ImageUrl = "2.jpg",
                    Genres = new List<Genre>()
                    {
                        genres[1],
                        genres[2],
                    }
                },
                new Movie
                {
                    //MovieID = 3,
                    Title = "Film 3",
                    Description = "Film 3 Açıklaması", /* Director = "Film 3 Açıklaması", */
                    // Players = new string[] { "Oyuncu 44", "Oyuncu 55", "Oyuncu 66" },
                    ImageUrl = "3.jpg",
                    Genres = new List<Genre>()
                    {
                        
                        genres[3]
                    }

                },
                new Movie
                {
                    //MovieID = 4,
                    Title = "Film 4",
                    Description = "Film 4 Açıklaması", /* Director = "Film Director", */
                    //Players = new string[] { "Oyuncu 1", "Oyuncu 2", "Oyuncu 3" },
                    ImageUrl = "4.webp",
                    Genres = new List<Genre>()
                    {
                        genres[1],
                        genres[2]
                    }
                },
                new Movie
                {
                    // MovieID = 5,
                    Title = "Film 5",
                    Description = "Film 5 Açıklaması", /* Director = "Film 2 Director", */
                    // Players = new string[] { "Oyuncu 11", "Oyuncu 22", "Oyuncu 33" },
                    ImageUrl = "5.webp",
                    Genres = new List<Genre>()
                    {
                        genres[1]
                    }
                },
                new Movie
                {
                    //MovieID = 6,
                    Title = "Film 6",
                    Description = "Film 6 Açıklaması", /* Director = "Film 3 Açıklaması", */
                    // Players = new string[] { "Oyuncu 44", "Oyuncu 55", "Oyuncu 66" },
                    ImageUrl = "6.webp",
                    Genres = new List<Genre>()
                    {
                        genres[0],
                        genres[1],
                        genres[2],
                        genres[3]
                    }
                }

            };

            var users = new List<User>()
            {
                new User
                {
                    UserName = "Engineer Enes",
                    Email = "enes@gmail.com",
                    Password = "1234",
                    ImageUrl = "enes.jpg"
                    /*
                    Person = new Person(){
                        Name = "Enes",
                        Biography = "kendisi bir mühendis",
                        HomePage = "eneskilic.com",
                        Imdb = "5.6",
                        PlaceOfBirth = "Kırkalreli",
                    */
                    //}

                },
                new User()
                {
                    UserName = "Emre",
                    Email = "emre@gmail.com",
                    Password = "1234",
                    ImageUrl = "emre.jpg"
                    //Person = new Person(){
                    //            Name = "Emre",
                    //            Biography = "kendisi biR öğrenci",
                    //            HomePage = "eneskilic.com",
                    //            Imdb = "5.6",
                    //            PlaceOfBirth = "Kırkalreli",
                    //    }
                }

            };

            
            var peoples = new List<Person>()
            {
                new Person()
                {
                    Name = "Enes",
                    Biography = "kendisi bir mühendis",
                    HomePage = "eneskilic.com",
                    Imdb = "5.6",
                    PlaceOfBirth = "Kırkalreli",
                    User = users[0]
                },
                new Person(){
                    Name = "Emre",
                    Biography = "kendisi biR öğrenci",
                    HomePage = "eneskilic.com",
                    Imdb = "5.6",
                    PlaceOfBirth = "Edirne",
                    User = users[1]
                }

            };

            var crews = new List<Crew>()
            {
                new Crew()
                {
                    Movie = movies[0],
                    Person = peoples[0],
                    Job = "Yönetmen"
                },
                new Crew()
                {
                    Movie = movies[0],
                    Person = peoples[1],
                    Job = "Yönetmen Yardımcısı"
                }
            };

            var casts = new List<Cast>()
            {
                new Cast()
                {
                    Movie = movies[0],  // aynı filmde oynayan b karakteri
                    Person = peoples[0],
                    Name = "Oyuncu adı 1",
                    Character = "Karakter 1"
                },
                new Cast()  // aynı filmde oynayan a karakteri
                {
                    Movie = movies[0],
                    Person = peoples[1],
                    Name = "Oyuncu adı 2",
                    Character = "Karakter 2"
                }
            };
           
            //bütün migrations uygulanmış bekleyen migrations yoksa
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                    // Genres
                    //**************************************************************
                    if (context.Genres.Count() == 0)
                    {
                        //context.Genres.AddRange(new List<Genre>()
                        //    {
                               
                        //    }
                        //);

                         context.Genres.AddRange(genres);   
                    }

                    if (context.Movies.Count() == 0) // daha önceden veritabanına kayıt eklenmişmi
                    {
                        context.Movies.AddRange
                        (
                            movies
                        );
                    }
                    if (context.Users.Count() == 0) // daha önceden veritabanına kayıt eklenmişmi
                    {
                        context.Users.AddRange
                        (
                            users
                        );
                    }
                    if (context.People.Count() == 0) // daha önceden veritabanına kayıt eklenmişmi
                    {
                        context.People.AddRange
                        (
                            peoples
                        );
                    }
                    if (context.Casts.Count() == 0) // daha önceden veritabanına kayıt eklenmişmi
                    {
                        context.Casts.AddRange
                        (
                            casts
                        );
                    }
                    if (context.Crews.Count() == 0) // daha önceden veritabanına kayıt eklenmişmi
                    {
                        context.Crews.AddRange
                        (
                            crews
                        );
                    }

                //**************************************************
                context.SaveChanges();
            }

        }
    }
}
