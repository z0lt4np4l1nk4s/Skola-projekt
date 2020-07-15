using SkolaProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkolaProjekt.Controllers
{
    public class DjelatnikController : Controller
    {
        // GET: Djelatnik
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            SkolaDBContext db = new SkolaDBContext();
            List<SelectListItem> skole = new List<SelectListItem>();

            foreach (Skola s in db.Skolas.ToList())
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = s.Naziv;
                selectListItem.Value = s.ID.ToString();
                skole.Add(selectListItem);
            }
            ViewData["SkolaID"] = skole;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Djelatnik djelatnik)
        {
            SkolaDBContext db = new SkolaDBContext();
            if (ModelState.IsValid)
            {
                db.Djelatniks.Add(djelatnik);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(djelatnik);
        }
    }
}