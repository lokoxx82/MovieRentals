using MovieRentals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieRentals.ViewModels;
using System.Data.Entity;

namespace MovieRentals.Controllers
{
    public class CustomersController : Controller
    {
        //Get the DB context
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //Dispose the context object correctly
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            List<Customer> customers = _context.Customers.Include(c=>c.MembershipType).ToList();
            CustomersViewModel viewModel = new CustomersViewModel{Customers = customers};
            return View(viewModel);
        }

        

        //Show customer details
        public ActionResult Detail(int id)
        {
            Customer customer = _context.Customers.FirstOrDefault(x=>x.Id==id);
            return View(customer);
        }

    }
}