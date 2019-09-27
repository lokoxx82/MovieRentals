﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MovieRentals.Dtos;
using MovieRentals.Models;

namespace MovieRentals.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto,Customer>().ForMember(x => x.Id, opt => opt.Ignore());
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MovieDto, Movie>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}