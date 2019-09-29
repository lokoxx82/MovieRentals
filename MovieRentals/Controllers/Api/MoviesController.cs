using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MovieRentals.Dtos;
using MovieRentals.Models;
using System.Data.Entity;

namespace MovieRentals.Controllers.Api
{
    public class MoviesController : ApiController
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


        //Get movies
        //GET/api/movies
        [Authorize]
        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies.Include(x=>x.Genre).ToList().Select(Mapper.Map<Movie, MovieDto>);
        }

        //get movie by id
        //GET/api/movies/id
        [Authorize]
        public IHttpActionResult GetMovie(int id)
        {
            Movie movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            if (movie == null) { return  NotFound();}

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //Create a movie
        //POST/api/movies
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid) { return BadRequest();}

            Movie movie = Mapper.Map<MovieDto, Movie>(movieDto);
            movie.Genre = _context.GenreTypes.FirstOrDefault(x => x.Id == movie.GenreTypeId);
            movie.NumberAvailable = movie.NumberInStock;

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }

        //Update existing movie
        //PUT/api/movies/1
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid) { throw new HttpResponseException(HttpStatusCode.BadRequest);}

            Movie movieInDb = _context.Movies.SingleOrDefault(x => x.Id == id);
            if (movieInDb == null) { throw  new  HttpResponseException(HttpStatusCode.NotFound);}

            //If stock amount changes then change the available movie count
            if (movieDto.NumberInStock != movieInDb.NumberInStock)
            {
                movieInDb.NumberAvailable -= movieInDb.NumberInStock - movieDto.NumberInStock;
            }
            Mapper.Map(movieDto,movieInDb);
            //movieInDb.Genre = _context.GenreTypes.FirstOrDefault(x => x.Id == movieInDb.GenreTypeId);
            _context.SaveChanges();
        }

        //Delete movie
        //DELETE/api/movies/1
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public void DeleteMovie(int id)
        {
            Movie movieInDb = _context.Movies.FirstOrDefault(x => x.Id == id);
            if (movieInDb == null) { BadRequest();}

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
        }
    }
}
