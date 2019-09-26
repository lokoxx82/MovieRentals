using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MovieRentals.Models;

namespace MovieRentals.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //Action to get customers
        //GET/Api/Customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        //Action to get a single customer
        //Get/Api/Customers/1
        public Customer GetCustomer(int id)
        {
            Customer customer = _context.Customers.FirstOrDefault(x => x.Id == id);
            if (customer == null) { throw  new HttpResponseException(HttpStatusCode.NotFound);}

            return customer;
        }

        //create a customer
        //POST/api/customers
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                throw  new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

        //Update an existing customer
        //PUT/api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid) { throw  new HttpResponseException(HttpStatusCode.BadRequest);}

            Customer customerInDb = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (customerInDb == null) { throw  new  HttpResponseException(HttpStatusCode.NotFound);}

            customerInDb.Name = customer.Name;
            customerInDb.BirthDate = customer.BirthDate;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;

            _context.SaveChanges();
        }

        //Delete a customer
        //DELETE/api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            Customer customerInDb = _context.Customers.FirstOrDefault(x => x.Id == id);
            if (customerInDb == null) { throw new HttpResponseException(HttpStatusCode.NotFound); }

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
            
        }
    }
}
