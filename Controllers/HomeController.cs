using SkolaProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkolaProjekt.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        SkolaDBContext db = new SkolaDBContext();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}