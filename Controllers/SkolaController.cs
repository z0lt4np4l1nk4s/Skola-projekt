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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Skola skola)
        {
            if (ModelState.IsValid)
            {
                SkolaDBContext db = new SkolaDBContext();
                db.Skola.Add(skola);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}