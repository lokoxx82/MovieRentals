using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieRentals.Models;
using MovieRentals.ViewModels;
using System.Data.Entity;
using System.Web.UI.WebControls;

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
            List<Movie> movies = _context.Movies.Include(x=>x.Genre).OrderBy(x=>x.Name).ToList();
            MoviesViewModel viewModel = new MoviesViewModel{Movies = movies};

            if (User.IsInRole(RoleName.CanManageMovies))
            {
                return View("List", viewModel);
            }

            return View("ReadOnlyList", viewModel);
        }

        //New movie
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            MovieFormViewModel viewModel = new MovieFormViewModel
            {
                GenreTypes = _context.GenreTypes.ToList(),
                Movie = new Movie()
            };
            return View("MovieForm", viewModel);
        }

        //Edit a movie
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            Movie movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            if (movie == null) return HttpNotFound();

            movie.Genre = _context.GenreTypes.FirstOrDefault(x => x.Id == movie.GenreTypeId);
            MovieFormViewModel viewModel = new MovieFormViewModel
            {
                Movie = movie,
                GenreTypes = _context.GenreTypes
            };
            return View("MovieForm", viewModel);
        }

        //Save the movie
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            movie.Genre = _context.GenreTypes.FirstOrDefault(x => x.Id == movie.GenreTypeId);
            movie.DateAdded = DateTime.Today;
            if (movie.Id == 0)
            {
                movie.ReleaseDate = DateTime.Today;
                movie.NumberAvailable = movie.NumberInStock;
                _context.Movies.Add(movie);
            }
            else
            {
                Movie movieInDb = _context.Movies.Single(x => x.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.Genre = movie.Genre;
                movieInDb.GenreTypeId = movie.GenreTypeId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.DateAdded = movie.DateAdded;
                if (movieInDb.NumberInStock != movie.NumberInStock)
                {
                    movieInDb.NumberAvailable-= movieInDb.NumberInStock - movie.NumberInStock;
                }
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        //Detail of a movie
        public ActionResult Detail(int id)
        {
            Movie movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            movie.Genre = _context.GenreTypes.FirstOrDefault(x => x.Id == movie.Id);
            return View(movie);
        }


        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {

            return Content(year + "   " + month);
        }

        
    }
}