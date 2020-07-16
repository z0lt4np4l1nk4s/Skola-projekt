using SkolaProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace SkolaProjekt.Controllers
{
    public class DjelatnikController : Controller
    {
        SkolaDBContext db = new SkolaDBContext();
        public ActionResult Create()
        {
            ListaSkoli();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Djelatnik djelatnik)
        {
            if (ModelState.IsValid)
            {
                db.Djelatniks.Add(djelatnik);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(djelatnik);
        }

        public ActionResult Edit(int id)
        {
            Djelatnik djelatnik = db.Djelatniks.Single(x => x.ID == id);
            ListaSkoli();
            return View(djelatnik);
        }

        private void ListaSkoli()
        {
            List<SelectListItem> skole = new List<SelectListItem>();

            foreach (Skola s in db.Skolas.ToList())
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = s.Naziv;
                selectListItem.Value = s.ID.ToString();
                skole.Add(selectListItem);
            }
            ViewData["SkolaID"] = skole;
        }

        [HttpPost]
        public ActionResult Edit(Djelatnik djelatnik)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(djelatnik).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            Djelatnik djelatnik = db.Djelatniks.Single(x => x.ID == id);
            return View(djelatnik);
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            Djelatnik djelatnik = db.Djelatniks.Single(x => x.ID == id);
            db.Djelatniks.Remove(djelatnik);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}