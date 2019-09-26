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
            CustomerFormViewModel formViewModel = new CustomerFormViewModel{MembershipTypes = membershipTypes};
            return View("CustomerForm",formViewModel);
        }

        //Save a new customer
        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            customer.MembershipType = _context.MembershipTypes.FirstOrDefault(x => x.Id == customer.MembershipTypeId);
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                Customer customerInDb = _context.Customers.Single(x => x.Id == customer.Id);
                customerInDb.MembershipType = customer.MembershipType;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.Name = customer.Name;
            }
            
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        //To edit a customer
        public ActionResult Edit(int id)
        {
            Customer customer = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (customer == null) return HttpNotFound();
            customer.MembershipType = _context.MembershipTypes.FirstOrDefault(x => x.Id == customer.Id);
            CustomerFormViewModel formViewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes
            };
            return View("CustomerForm", formViewModel);
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