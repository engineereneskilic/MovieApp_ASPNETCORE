using Microsoft.AspNetCore.Mvc;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Linq;
using MovieApp.Web.Data;
using MovieApp.Web.Entity;

namespace MovieApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieContext _context;

        public HomeController(MovieContext movieContext)
        {
            _context = movieContext;
        }


        public IActionResult Index()
        {
            var model = new HomePageViewModel()
            {
                //PopularMovies = MovieRepository.Movies
                PopularMovies = _context.Movies.ToList()
            };

            return View(model);
        }

        public IActionResult About()
        {
            
            return View();
        }
    }
}
