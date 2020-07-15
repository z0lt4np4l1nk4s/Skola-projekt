using SkolaProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkolaProjekt.Controllers
{
    public class SkolaController : Controller
    {
        // GET: Skola
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Skola skola)
        {
            SkolaDBContext db = new SkolaDBContext();
            if (ModelState.IsValid)
            {

                db.Skolas.Add(skola);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(skola);
        }
    }
}