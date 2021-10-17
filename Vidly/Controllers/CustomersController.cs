using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO.MemoryMappedFiles;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {

        public ApplicationDbContext context { get; set; }

        public CustomersController()
        {
            context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            var customers = context.Customers.Include(c => c.Membership).ToList();
            var t = context.Customers.Local;

            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = context.Customers.Include(c=>c.Membership).SingleOrDefault(c => c.Id == id);
            if (customer != null)
            {
                return View(customer);
            }

            return HttpNotFound();

        }


        public ActionResult Edit(int  id)
        {
            var membershipTypes = from m in context.Memberships
                select m;
            var customer = context.Customers.SingleOrDefault(c=>c.Id==id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            CustomerFormViewModel formViewModel = new CustomerFormViewModel
            {
                Customer = customer,
                Memberships = membershipTypes
            };
            return View("CustomerForm", formViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var membershipTypes = from m in context.Memberships
                select m;

            CustomerFormViewModel formViewModel = new CustomerFormViewModel
            {
                Customer = new Customer(), // To not get an error about id anymore
                Memberships = membershipTypes

            };


            return View("CustomerForm",formViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View("CustomerForm", new CustomerFormViewModel() { Customer = customer, Memberships = context.Memberships.ToList() });
            }


            if (customer.Id == 0)
            {
                context.Customers.Add(customer);
            }
            else
            {
                var customerDb = context.Customers.SingleOrDefault(c => c.Id == customer.Id);
                customerDb.Name = customer.Name;
                customerDb.Birthdate = customer.Birthdate;
                customerDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                customerDb.MembershipId = customer.MembershipId;
            }

            context.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}