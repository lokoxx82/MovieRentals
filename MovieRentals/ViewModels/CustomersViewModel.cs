using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieRentals.Models;

namespace MovieRentals.ViewModels
{
    public class CustomersViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
    }
}