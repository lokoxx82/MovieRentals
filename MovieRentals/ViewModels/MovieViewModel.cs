using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieRentals.Models;

namespace MovieRentals.ViewModels
{
    public class MovieViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
    }
}