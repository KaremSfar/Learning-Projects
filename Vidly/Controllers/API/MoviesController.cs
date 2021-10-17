using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTOs;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.API
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext context;

        public MoviesController()
        {
            context = new ApplicationDbContext();
        }


        //GET /api/movies
        public IHttpActionResult GetMovies(string query = null)
        {
            var moviesQuery = context.Movies.Include(m => m.Genre).Where(m => m.Available > 0);

            if (!string.IsNullOrWhiteSpace(query))
            {
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));
            }

            var movies = moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
            return Ok(movies);
        }


        //GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                NotFound();
            }
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }


        //POST /api/movies
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            var movie = context.Movies.Add(Mapper.Map<MovieDto, Movie>(movieDto));
            context.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }


        //PUT /api/movies/id
        [Authorize(Roles = RoleName.CanManageMovies)]
        public void UpdateMovie(int id,MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            var movieDb = context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movieDb == null)
            {
                NotFound();
            }

            Mapper.Map(movieDb, movieDto);
            context.SaveChanges();
        }


        //DELETE /api/movies/id
        [Authorize(Roles = RoleName.CanManageMovies)]
        public void Delete(int id)
        {
            var movieDb = context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieDb == null)
            {
                NotFound();
            }

            context.Movies.Remove(movieDb);
            context.SaveChanges();
        } 
    }
}
