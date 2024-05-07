using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Web.Data;
using MovieApp.Web.Models;

namespace MovieApp.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly MovieContext _movieContext;

        public AdminController(MovieContext context)
        {
            _movieContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MovieList()
        {
            return View(new AdminMoviesViewModel()
            {
                Movies = _movieContext.Movies
               .Include(m => m.Genres)
               .Select(m => new AdminMovieViewModel
               {
                   MovieID = m.MovieID,
                   Title = m.Title,
                   ImageURL = m.ImageUrl,
                   Genres = m.Genres.ToList()
               })
               .ToList()
            });
        }

        public IActionResult MovieCreate()
        {
            ViewBag.Genres = _movieContext.Genres.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult MovieCreate(AdminCreateMovieModel model)
        {
            if (model.Title != null && model.Title.Contains("@"))
            {
                ModelState.AddModelError("Title", "Film Başlığı @ işareti içeremez");
            }

            //if(model.GenreIds == null)
            //{
            //    ModelState.AddModelError("", "En az bir tür seçmelisiniz");
            //}

            if (ModelState.IsValid)
            {
                var entity = new Entity.Movie
                {
                    Title = model.Title,
                    Description = model.Description,
                    ImageUrl = "no-img.jpg"
                };

                // Movie listesi null olmasın boş bir nesne olsun
                //entity.Genres = new List<Entity.Genre>();
                foreach (var id in model.GenreIds)
                {
                    // ilgili fimlin türler listesine ekle
                    entity.Genres.Add(_movieContext.Genres.FirstOrDefault(i => i.GenreID == id));
                }
                _movieContext.Movies.Add(entity);
                _movieContext.SaveChanges();

                return RedirectToAction("MovieList", "Admin");
            }
            ViewBag.Genres = _movieContext.Genres.ToList();

            return View(model);
        }



        [HttpGet]
        public IActionResult MovieUpdate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _movieContext.Movies.Select(m => new AdminEditViewModel
            {
                MovieID = m.MovieID,
                Title = m.Title,
                Description = m.Description,
                ImageURL = m.ImageUrl,
                GenreIds = m.Genres.Select(i => i.GenreID).ToArray()

            }).FirstOrDefault(m => m.MovieID == id);

            ViewBag.Genres = _movieContext.Genres.ToList();

            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);

        }

        [HttpPost]
        public async Task<IActionResult> MovieUpdate(AdminEditViewModel adminEditViewModel, int[] genreIds, IFormFile? movieCoverImageFile)
        {
            if (ModelState.IsValid)
            {
                // Genres artık tracking(takip) ediliyor artık o nedenle istediğimizi yapabiliriz
                var entity = _movieContext.Movies.Include(m => m.Genres).FirstOrDefault(m => m.MovieID == adminEditViewModel.MovieID);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Title = adminEditViewModel.Title;
                entity.Description = adminEditViewModel.Description;

                if (movieCoverImageFile != null)
                {
                    var extension = Path.GetExtension(movieCoverImageFile.FileName); // .jpg , .png
                    var fileName = string.Format($"{Guid.NewGuid()}{extension}");

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", movieCoverImageFile.FileName);
                    entity.ImageUrl = movieCoverImageFile.FileName;

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await movieCoverImageFile.CopyToAsync(stream);
                    }
                }

                //entity.Genres = new List<Entity.Genre>();
                entity.Genres = genreIds.Select(id => _movieContext.Genres.FirstOrDefault(i => i.GenreID == id)).ToList();

                _movieContext.SaveChanges();


                return RedirectToAction("MovieList", "Admin");
            }
            ViewBag.Genres = _movieContext.Genres.ToList();

            return View(adminEditViewModel);
        }

        [HttpPost]
        public IActionResult MovieDelete(int movieId)
        {
            var entity = _movieContext.Movies.Find(movieId);

            if (entity != null)
            {
                _movieContext.Movies.Remove(entity);
                _movieContext.SaveChanges();
            }

            return RedirectToAction("MovieList");
        }

        // Genre********------------------------------------------------------------------------------------------------

        [HttpPost] 
        public IActionResult GenreCreate(AdminGenresViewModel model)
        {
            if (model.Name != null && model.Name.Length < 3)
            {
                ModelState.AddModelError("Name","Tür Adı Minimum 3 karakterli olmalıdır");
            }
            if (ModelState.IsValid)
            {
                _movieContext.Genres.Add(new Entity.Genre { Name = model.Name });
                _movieContext.SaveChanges();

                return RedirectToAction("GenreList");
            }
            return View("GenreList", GetGenres()); // fonkstion gibi kullanıor
        }

        private AdminGenresViewModel GetGenres()
        {
            return new AdminGenresViewModel()
            {
                Genres = _movieContext.Genres.Select(g => new AdminGenreViewModel()
                {
                    GenreID = g.GenreID,
                    Name = g.Name,
                    Count = g.Movies.Count()

                }).ToList()
            };
        }

        public IActionResult GenreList()
        {

            return View(GetGenres());
        }

        public IActionResult GenreUpdate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _movieContext
                .Genres
                .Select(g => new AdminGenreEditViewModel
                {
                    GenreID = g.GenreID,
                    Name = g.Name,
                    Movies = g.Movies.Select(i => new AdminMovieViewModel
                    {
                        MovieID = i.MovieID,
                        Title = i.Title,
                        ImageURL = i.ImageUrl


                    }).ToList()

                })
                .FirstOrDefault(g => g.GenreID == id);

            if (entity == null)
            {
                return NotFound();
            }


            return View(entity);
        }

        [HttpPost]
        public IActionResult GenreUpdate(AdminGenreEditViewModel model, int[] movieIDs)
        {
            
            if (ModelState.IsValid)
            {
                var entity = _movieContext.Genres.Include("Movies").FirstOrDefault(i => i.GenreID == model.GenreID);

                if (entity == null)
                {
                    return NotFound();
                }

                entity.Name = model.Name;
                foreach (var id in movieIDs)
                {
                    entity.Movies.Remove(entity.Movies.FirstOrDefault(i => i.MovieID == id));
                }

                _movieContext.SaveChanges();
                Console.WriteLine("Kayıt eklendi");

                return RedirectToAction("GenreList");
            }

            return View(model);
        }

        // Delete **********************
        [HttpPost]
        public IActionResult GenreDelete(int genreId)
        {
            var entity = _movieContext.Genres.Find(genreId);

            if(entity != null)
            {
                _movieContext.Genres.Remove(entity);
                _movieContext.SaveChanges();
            }

            return RedirectToAction("GenreList");
        }
    }
}
