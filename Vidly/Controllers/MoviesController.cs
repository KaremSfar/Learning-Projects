using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Web.Razor;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        public ApplicationDbContext context { get; set; }
        public MoviesController()
        {
            this.context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            Movie movie = new Movie() {Name = "Alouuu"};

            List<Customer> customers = new List<Customer>()
            {
                new Customer(){Id =2,Name = "TZ"},
                new Customer(){Id=2,Name = "Bah wé"}
            };

            RandomMovieViewModel viewModel = new RandomMovieViewModel() {Movie = movie, Customers = customers};


            return View(viewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var genres = from g in context.Genres
                select g;

            var movie = context.Movies.Include(m => m.Genre).Single(m => m.Id == id);

            MovieFormViewModel viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = genres
            };

            return View("MovieForm", viewModel);


        }


        //Movies
        // /Movies/1/Name only works if custom route defined
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            var movies = from m in context.Movies.Include(m => m.Genre)
                select m;
            var list = movies.ToList();

            if (!User.IsInRole("CanManageMovies"))
            {
                return View("IndexRestricted", list);
            }

            return View(list);

        }

        public ActionResult Release(int month, int year)
        {
            return Content($"{month} / {year}");
        }

        public ActionResult Detail(int id)
        {
            var movie = from m in context.Movies.Include(m => m.Genre)
                where m.Id == id
                select m;
            return View(movie.Single());
        }

        [HttpGet]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Create()
        {
            var genres = from g in context.Genres
                select g;
            MovieFormViewModel viewModel = new MovieFormViewModel
            {
                Movie = new Movie(),
                Genres = genres
            };
            return View("MovieForm",viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {

            if (!ModelState.IsValid)
            {
                var movieForm = new MovieFormViewModel()
                {
                    Movie = movie,
                    Genres = context.Genres.ToList()
                };
                return View("MovieForm", movieForm);
            }

            var genre = from g in context.Genres
                where g.Id == movie.Genre.Id
                select g;
            movie.Genre = genre.Single();
            movie.Available = movie.Stock;
            if (movie.Id == 0)
            {
                context.Movies.Add(movie);
            }
            else
            {
                var movieDb = context.Movies.Include(m => m.Genre).Single(m => m.Id == movie.Id);
                movieDb.Name = movie.Name;
                movieDb.DateAdded = movie.DateAdded;
                movieDb.Genre = movie.Genre;
                movieDb.ReleaseDate = movie.ReleaseDate;
                movieDb.Stock = movie.Stock;

            }

            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}