using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTOs;
using Vidly.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Web.Http.Results;

namespace Vidly.Controllers.API
{
    public class RentalsController : ApiController
    {
        private ApplicationDbContext context;

        public RentalsController()
        {
            context = new ApplicationDbContext();
        }

        //GET /api/rentals
        [HttpGet]
        public IHttpActionResult GetRentals()
        {
            var rentals = context.Rentals.Include(r => r.Customer).Include(r => r.Movie);
            return Ok(rentals);
        }


        //POST /api/rentals
        [HttpPost]
        public IHttpActionResult CreateNewRental(NewRentalDto newRental)
        {
            //if (newRental.MoviesIds.Count == 0)
            //{
            //    return BadRequest("No Movies Ids have been given.");
            //}

            var customer = context.Customers.Single(c => c.Id == newRental.CustomerId);

            var movies = context.Movies.Include(m=>m.Genre).Where(
                m => newRental.MoviesIds.Contains(m.Id)).ToList();

            //if (movies.Count != newRental.MoviesIds.Count)
            //{
            //    return BadRequest("One or more movies are unavailable");
            //}

            newRental.MoviesIds = new List<int>();

            foreach(var movie in movies)
            {
                if (movie.Available > 0)
                {
                    movie.Available -= 1;
                    var rental = new Rental
                    {
                        Customer = customer,
                        Movie = movie,
                        DateRented = DateTime.Now
                    };

                    context.Rentals.Add(rental);

                    newRental.MoviesIds.Add(movie.Id);
                }
            }
            context.SaveChanges();
            return Ok<NewRentalDto>(newRental);
        }

    }
}
