using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MovieRentals.Dtos;
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
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customer,CustomerDto>);
        }

        //Action to get a single customer
        //Get/Api/Customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = _context.Customers.FirstOrDefault(x => x.Id == id);
            if (customer == null) { return  NotFound();}

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //create a customer
        //POST/api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Customer customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            customer.MembershipType = _context.MembershipTypes.FirstOrDefault(x => x.Id == customer.MembershipTypeId);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customerDto.Id), customerDto);
        }

        //Update an existing customer
        //PUT/api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid) { throw  new HttpResponseException(HttpStatusCode.BadRequest);}

            Customer customerInDb = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (customerInDb == null) { throw  new  HttpResponseException(HttpStatusCode.NotFound);}

            Mapper.Map(customerDto, customerInDb);
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
