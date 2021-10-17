using AutoMapper;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        private ApplicationDbContext context;

        public MappingProfile()
        {
            context = new ApplicationDbContext();

            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer>();
            Mapper.CreateMap<Membership, MembershipDto>();
            //Uses Property names to map objects
            //Needs to start in Application Start in global asax

            Mapper.CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.GenreId,
                opts => opts.MapFrom(src => src.Genre.Id));

            Mapper.CreateMap<MovieDto, Movie>()
                .ForMember(dest=>dest.Genre,
                opts => opts.MapFrom(src => context.Genres.SingleOrDefault(g => g.Id == src.GenreId)
                ))
                .ForMember(dest=>dest.Available,
                opts=>opts.MapFrom(src=>src.Stock));

        }
    }
}