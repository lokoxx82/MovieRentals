using MovieRentals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieRentals.ViewModels;

namespace MovieRentals.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            List<Customer> customers = GetCustomers();
            CustomersViewModel viewModel = new CustomersViewModel{Customers = customers};
            return View(viewModel);
        }

        

        //Show customer details
        public ActionResult Detail(int id)
        {
            List<Customer> customers = GetCustomers();
            Customer customer = customers.FirstOrDefault(x => x.Id == id);
            return View(customer);
        }


        private static List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer {Id = 1, Name = "Fero"},
                new Customer {Id = 2, Name = "Jozo"},
                new Customer {Id = 3, Name = "Martin"}
            };
            return customers;
        }
    }
}