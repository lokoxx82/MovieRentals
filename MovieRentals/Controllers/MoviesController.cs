using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieRentals.Models;
using MovieRentals.ViewModels;

namespace MovieRentals.Controllers
{
    public class MoviesController : Controller
    {
        //Index list of movies
        public ActionResult Index()
        {
            List<Movie> movies = new List<Movie>
            {
                new Movie{Id = 1,Name = "Kill bill"},
                new Movie{Id = 2,Name = "Kill bill 2"}

            };
            MovieViewModel viewModel = new MovieViewModel{Movies = movies};
            return View(viewModel);
        }

        // GET: Random movie
        public ActionResult Random()
        {
            List<Movie> movies = new List<Movie>
            {
                new Movie{Id = 1,Name = "Kill bill"},
                new Movie{Id = 2,Name = "Kill bill 2"}
                
            };

            List<Customer> customers = new List<Customer>
            {
                new Customer{Id = 1, Name = "Fero"},
                new Customer{Id = 2,Name = "Jozo"}
            };

            RandomViewModel viewModel = new RandomViewModel
            {
                Customers = customers,
                Movie = movies[1]
            };
            return View(viewModel);
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