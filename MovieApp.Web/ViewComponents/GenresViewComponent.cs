using Microsoft.AspNetCore.Mvc;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Linq;
using MovieApp.Web.Data;

namespace MovieApp.Web.ViewComponents
{
    public class GenresViewComponent: ViewComponent
    {
        private readonly MovieContext _context;

        public GenresViewComponent(MovieContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedGenreID = RouteData.Values["id"];
            return View(_context.Genres.ToList());
        }
    }
}
