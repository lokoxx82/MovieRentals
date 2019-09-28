using MovieRentals.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRentals.Dtos
{
    public class MovieDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public GenreTypeDto Genre { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public byte GenreTypeId { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        [Range(1, 20)]
        public int NumberInStock { get; set; }
    }
}