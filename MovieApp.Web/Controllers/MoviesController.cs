using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApp.Web.Data;
using MovieApp.Web.Data;
using MovieApp.Web.Entity;
using MovieApp.Web.Models;
using Movie = MovieApp.Web.Entity.Movie;

namespace MovieApp.Web.Controllers
{
    
    // contoller
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            // inject ile göndermiş olduğumuz nesneyi eşitlemek
            _context = context;
        }
        //action
        //localhost: 5000/ movies / id
        //localhost: 5000/ movies / id
        [HttpGet]
        public IActionResult Index()
        {
            var movies = _context.Movies.AsQueryable();
            var model = new MoviesViewModel()
            {
                Movies = movies
            };

            return View("Movies",model);
        }
        [HttpGet]
        public IActionResult List(int? id,string q)
        {
             var movies = _context.Movies.AsQueryable(); // sorgulanabilir
            //{controller}/{action}/{id?}
            // movies/list/3

            var controller = RouteData.Values["controller"];
            var action = RouteData.Values["action"];
            var genreid = RouteData.Values["id"];

            var aranan_kelime = q;
            //var aranan_kelime = HttpContext.Request.Query["q"].ToString();

            //var movies = MovieRepository.Movies;

            if (id != null)
            {
                movies = movies
                    .Include(m => m.Genres)  // gelen her bir filmin ilişkili olduğu tür bilgisini almak istiyoruz
                    .Where(m => m.Genres.Any(g => g.GenreID == id));
                    //ilgili filmin genre listesinin her bir elemanın genre idsi yukardan gönderilen değere eşit mi
                    // eğer eşitse o filmi al son istediğim listeye at
            }
            else if(!string.IsNullOrEmpty(aranan_kelime))
            {
                movies = movies.Where(
                    i => i.Title.ToLower().Contains(q.ToLower()) || 
                         i.Description.ToLower().Contains(q.ToLower()) );
            }
            var model = new MoviesViewModel()
            {
                Movies = movies
            };

            return View("Movies",model);
        }


        //localhost: 5000/ movies / details / 1
        // id direk startup'taki id olcak
        [HttpGet]
        public IActionResult Details(int id)
        {
           // return View("Details",MovieRepository.GetByID(id));
           return View("Details", _context.Movies.Find(id));
        }

        //CREATE--------------------------------------------------------


        //[HttpGet]
        //public IActionResult Create()
        //{
        //    ViewBag.Genres = new SelectList(_context.Genres.ToList(), "GenreID", "Name");
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Create(Movie m)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        TempData["Message"] = $"{m.Title} isimli film eklendi.";
        //        _context.Movies.Add(m);
        //        _context.SaveChanges();
        //        //MovieRepository.Add(m);
        //        return RedirectToAction("List", "Movies");
        //    }
        //    return View();

        //}

        //EDIT--------------------------------------------------------

        //[HttpGet]
        //public IActionResult Edit(int id) // model binding
        //{
        //   // var selected_model = MovieRepository.GetByID(id);
        //   var selected_model = _context.Movies.Find(id);

        //    var editPageViewModel = new EditPageViewModel()
        //    {
        //        Genres = GenreRepository.Genres,
        //        Movie = selected_model
        //    };

        //    //ViewBag.Genres = new SelectList(GenreRepository.Genres, "GenreId", "Name");
        //    ViewBag.Genres = new SelectList(_context.Genres.ToList(), "GenreID", "Name");

        //    return View(editPageViewModel);
        //}

        //[HttpPost]
        //public IActionResult Edit(Movie m)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        MovieRepository.Edit(m);
        //        _context.Movies.Update(m);
        //        _context.SaveChanges();
        //        return RedirectToAction("Details", "Movies", new { @id = m.MovieID });
        //    }
        //    //ViewBag.Genres = new SelectList(GenreRepository.Genres, "GenreId", "Name");
        //    ViewBag.Genres = new SelectList(_context.Genres.ToList(), "GenreID", "Name");

        //    return View();
        //}

        //DELETE--------------------------------------------------------
        //[HttpPost]
        //public IActionResult Delete(int MovieId, string Title)
        //{
        //    //MovieRepository.Delete(MovieId);
        //    //ViewBag.Message = $"{Title} isimli film Silindi";
        //    var _entitiy = _context.Movies.Find(MovieId);
        //    _context.Movies.Remove(_entitiy);
        //    _context.SaveChanges();
        //    TempData["Message"] = $"{Title} isimli film silindi.";

        //    return RedirectToAction("List", "Movies");
        //}

        // GENRE LİST ************************************************************************

        public IActionResult GenreList()
        {
            return View();
        }
    }
}
