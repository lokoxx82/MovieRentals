using MovieRentals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieRentals.ViewModels;
using System.Data.Entity;
using System.Web.UI.WebControls;
using System.Runtime.Caching;

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
            if (MemoryCache.Default["MembershipTypes"]==null)
            {
                MemoryCache.Default["MembershipTypes"] = _context.MembershipTypes.ToList();
            } ;
            IEnumerable<MembershipType> membershipTypes = (IEnumerable<MembershipType>)MemoryCache.Default["MembershipTypes"];

            //CustomersViewModel viewModel = new CustomersViewModel
            //{
            //    Customers = _context.Customers.Include(c => c.MembershipType).OrderBy(x=>x.Name).ToList()
            //};
            //return View(viewModel);
            return View();
        }

        //New customer
        public ActionResult New()
        {
            CustomerFormViewModel viewModel = new CustomerFormViewModel{MembershipTypes = _context.MembershipTypes.ToList() };
            return View("CustomerForm",viewModel);
        }

        //Save a new customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            customer.MembershipType = _context.MembershipTypes.FirstOrDefault(x => x.Id == customer.MembershipTypeId);
            //if (!ModelState.IsValid)
            //{
            //    CustomerFormViewModel viewModel = new CustomerFormViewModel
            //    {
            //        Customer = customer,
            //        MembershipTypes = _context.MembershipTypes.ToList()
            //    };
            //    return View("CustomerForm", viewModel);

            //}

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