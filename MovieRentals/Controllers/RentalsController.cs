using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieRentals.Models;

namespace MovieRentals.Controllers
{
    public class RentalsController : Controller
    {
        // GET: Rentals
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            return View();
        }
    }
}