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
            Customer customer = _context.Customers.SingleOrDefault(x => x.Id == newRentalDto.CustomerId);
            if (customer == null) { BadRequest("No customer found");}

            IQueryable<Movie> moviesInDb = _context.Movies.Where(x => newRentalDto.MovieIds.Contains(x.Id));

            foreach (Movie movie in moviesInDb)
            {
                //Create a new Rental object
                Rental rental = new Rental
                {
                    Customer = customer,
                    DateRented = DateTime.Now,
                    Movie = movie
                };
                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();
            return Ok();
        }
    }
}
