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
        //Rent movies
        //Post/Api/rentals
        [HttpPost]
        [Authorize(Users = RoleName.CanManageMovies)]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRentalDto)
        {

            return Created(new Uri(Request.RequestUri.ToString()), newRentalDto);
        }
    }
}
