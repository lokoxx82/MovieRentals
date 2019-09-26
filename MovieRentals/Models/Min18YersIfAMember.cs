using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRentals.Models
{
    public class Min18YersIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Customer customer = (Customer) validationContext.ObjectInstance;
            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if (customer.BirthDate == null)
            {
                return new ValidationResult("Birth date is required");
            }
            if  (DateTime.Today.Year - customer.BirthDate.Value.Year < 18)
            {
                return new ValidationResult("The customer need to be at least 18 years old" );
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}