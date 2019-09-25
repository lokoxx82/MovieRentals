using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieRentals.Models;
using MovieRentals.ViewModels;
using System.Data.Entity;

namespace MovieRentals.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //Index list of movies
        public ActionResult Index()
        {
            List<Movie> movies = _context.Movies.Include(x=>x.Genre).ToList();
            MoviesViewModel viewModel = new MoviesViewModel{Movies = movies};
            return View(viewModel);
        }


        //Detail of a movie
        public ActionResult Detail(int id)
        {
            Movie movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            movie.Genre = _context.GenreTypes.FirstOrDefault(x => x.Id == movie.Id);
            return View(movie);
        }


        //Edit a movie
        public ActionResult Edit(int id)
        {
            return Content(id.ToString());
        }

        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {

            return Content(year + "   " + month);
        }
    }
}