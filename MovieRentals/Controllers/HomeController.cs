using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MovieRentals.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        //Caching is disabled because of 0, for anabling set time and delete NoStore = true
        [OutputCache(Duration = 0, Location = OutputCacheLocation.Server,VaryByParam = "*", NoStore = true)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            throw  new Exception();
            return Content("");
        }
    }
}