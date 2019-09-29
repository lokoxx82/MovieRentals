using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MovieRentals.Dtos;
using MovieRentals.Models;

namespace MovieRentals.Controllers.Api
{
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context=new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //Rent movies
        //Post/Api/rentals
        [HttpPost]
        //[Authorize(Users = RoleName.CanManageMovies)]
        [AllowAnonymous]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRentalDto)
        {
            //Commented out checks are not necessary, they would be good for public API, not my internal
            //if (newRentalDto.MovieIds.Count == 0) { BadRequest("No movies selected to rent."); }
            Customer customer = _context.Customers.SingleOrDefault(x => x.Id == newRentalDto.CustomerId);
            if (customer == null) { BadRequest("No customer found");}
            
            List<Movie> moviesInDb = _context.Movies.Where(x => newRentalDto.MovieIds.Contains(x.Id)).ToList();
            //if (moviesInDb.Count != newRentalDto.MovieIds.Count) { return BadRequest("Some movies are not found in the db");}

            foreach (Movie movie in moviesInDb)
            {
                if (movie.NumberAvailable == 0) { return  BadRequest("Movie " + movie.Name + "is not available.");}
                //Create a new Rental object
                Rental rental = new Rental
                {
                    Customer = customer,
                    DateRented = DateTime.Now,
                    Movie = movie
                };
                movie.NumberAvailable--;
                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();
            return Ok();
        }
    }
}
