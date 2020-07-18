using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkolaProjekt.Models;

namespace SkolaProjekt.Controllers
{
    public class DjelatniksController : Controller
    {
        private SkolaDBContext db = new SkolaDBContext();
        // GET: Djelatniks/Create
        public ActionResult Index(string search)
        {
            ViewBag.Skole = db.Skola;
            ViewBag.SelectedSkole = db.DjelatnikSkola.ToList();
            var c = db.Djelatnik.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                c = c.Where(x => x.Ime.StartsWith(search) || x.Prezime.StartsWith(search) || x.Mjesto.StartsWith(search));
            }
            return View(c.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.Skole = db.Skola;
            return View();
        }

        // POST: Djelatniks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Ime,Prezime,Mjesto,Zanimanje")] Djelatnik djelatnik, int[] SkolaID)
        {
            if (ModelState.IsValid)
            {
                db.Djelatnik.Add(djelatnik);
                db.SaveChanges();
                Djelatnik dj = db.Djelatnik.Single(x => x.Ime == djelatnik.Ime && x.Prezime == djelatnik.Prezime && x.Mjesto == djelatnik.Mjesto && x.Zanimanje == djelatnik.Zanimanje);
                foreach (int i in SkolaID)
                {
                    DjelatnikSkola djelatnikSkola = new DjelatnikSkola();
                    djelatnikSkola.IDDjelatnik = dj.ID;
                    djelatnikSkola.IDSkola = i;
                    db.DjelatnikSkola.Add(djelatnikSkola);
                }
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(djelatnik);
        }

        // GET: Djelatniks/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Skole = db.Skola;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Djelatnik djelatnik = db.Djelatnik.Single(x => x.ID == id);
            if (djelatnik == null)
            {
                return HttpNotFound();
            }
            ViewBag.SelectedSkole = db.DjelatnikSkola;
            return View(djelatnik);
        }

        // POST: Djelatniks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Ime,Prezime,Mjesto,Zanimanje")] Djelatnik djelatnik, int[] SkolaID)
        {
            if (ModelState.IsValid)
            {
                foreach (DjelatnikSkola ds in db.DjelatnikSkola.ToList())
                {
                    if (ds.IDDjelatnik == djelatnik.ID)
                    {
                        db.DjelatnikSkola.Remove(ds);
                    }
                }
                foreach (int i in SkolaID)
                {
                    DjelatnikSkola djelatnikSkola = new DjelatnikSkola();
                    djelatnikSkola.IDDjelatnik = djelatnik.ID;
                    djelatnikSkola.IDSkola = i;
                    db.DjelatnikSkola.Add(djelatnikSkola);
                }
                db.Entry(djelatnik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(djelatnik);
        }

        // GET: Djelatniks/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.Skole = db.Skola;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Djelatnik djelatnik = db.Djelatnik.Find(id);
            ViewBag.SelectedSkole = db.DjelatnikSkola.ToList().Where(x => x.IDDjelatnik == id);
            if (djelatnik == null)
            {
                return HttpNotFound();
            }
            return View(djelatnik);
        }

        // POST: Djelatniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Djelatnik djelatnik = db.Djelatnik.Find(id);
            foreach (DjelatnikSkola ds in db.DjelatnikSkola.ToList())
            {
                if (ds.IDDjelatnik == djelatnik.ID)
                {
                    db.DjelatnikSkola.Remove(ds);
                }
            }
            db.Djelatnik.Remove(djelatnik);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
