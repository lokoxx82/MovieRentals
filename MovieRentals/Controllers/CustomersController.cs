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
            CustomersViewModel viewModel = new CustomersViewModel
            {
                Customers = _context.Customers.Include(c => c.MembershipType).ToList()
        };
            return View(viewModel);
        }

        //New customer
        public ActionResult New()
        {
            List<MembershipType> membershipTypes = _context.MembershipTypes.ToList();
            NewCustomerViewModel viewModel = new NewCustomerViewModel{MembershipTypes = membershipTypes};
            return View(viewModel);
        }

        //Create a new customer
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            customer.MembershipType = _context.MembershipTypes.FirstOrDefault(x => x.Id == customer.MembershipTypeId);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }


        //Show customer details
        public ActionResult Detail(int id)
        {
            Customer customer = _context.Customers.FirstOrDefault(x=>x.Id==id);
            customer.MembershipType = _context.MembershipTypes.FirstOrDefault(x => x.Id == customer.MembershipTypeId);
            return View(customer);
        }

    }
}