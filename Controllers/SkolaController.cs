using SkolaProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace SkolaProject.Controllers
{
    [Authorize]
    public class SkolaController : Controller
    {
        SkolaDBContext db = new SkolaDBContext();
        public ActionResult Index(string search, int? page)
        {
            ViewBag.SviDjelatnici = db.Djelatnik;
            ViewBag.SelectedDjelatnici = db.DjelatnikSkola;
            var s = db.Skola.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                s = s.Where(x => x.Naziv.ToLower().StartsWith(search.ToLower()) || x.Mjesto.ToLower().StartsWith(search.ToLower()));
            }
            return View(s.ToList().ToPagedList(page ?? 1, 5));
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Skola skola)
        {
            if (ModelState.IsValid)
            {
                db.Skola.Add(skola);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}