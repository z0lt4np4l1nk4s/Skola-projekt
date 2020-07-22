using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkolaProjekt.Models;
using PagedList;
using System.IO;
using System.Security.Permissions;
using System.Data.Entity.Migrations;

namespace SkolaProject.Controllers
{
    [Authorize]
    public class DjelatnikController : Controller
    {
        private SkolaDBContext db = new SkolaDBContext();
        // GET: Djelatniks/Create
        public ActionResult Index(string search, string sortBy, int? page)
        {
            ViewBag.Skole = db.Skola;
            ViewBag.SelectedSkole = db.DjelatnikSkola;

            ViewBag.SortIme = sortBy == "Ime" ? "Ime desc" : "Ime";
            ViewBag.SortPrezime = sortBy == "Prezime" ? "Prezime desc" : "Prezime";
            var c = db.Djelatnik.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                c = c.Where(x => x.Ime.ToLower().StartsWith(search.ToLower()) || x.Prezime.ToLower().StartsWith(search.ToLower()) || x.Mjesto.ToLower().StartsWith(search.ToLower()));
            }

            switch (sortBy)
            {
                case "Ime":
                    c = c.OrderBy(x => x.Ime);
                    break;
                case "Ime desc":
                    c = c.OrderByDescending(x => x.Ime);
                    break;
                case "Prezime":
                    c = c.OrderBy(x => x.Prezime);
                    break;
                case "Prezime desc":
                    c = c.OrderByDescending(x => x.Prezime);
                    break;
                default:
                    c = c.OrderBy(x => x.ID);
                    break;
            }

            return View(c.ToList().ToPagedList(page ?? 1, 5));
        }

        [Authorize(Roles = "Admin")]
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
        public ActionResult Create([Bind(Include = "ID,Ime,Prezime,Mjesto,Zanimanje,SlikaPath,SlikaFile")] Djelatnik djelatnik, int[] SkolaID)
        {
            if (ModelState.IsValid)
            {
                if (djelatnik.SlikaFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(djelatnik.SlikaFile.FileName) + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(djelatnik.SlikaFile.FileName);
                    djelatnik.SlikaPath = "~/Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    djelatnik.SlikaFile.SaveAs(fileName);
                }
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
                return RedirectToAction("Index");
            }

            return View(djelatnik);
        }

        public ActionResult Details(int? id)
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

        [Authorize(Roles = "Admin")]
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
        public ActionResult Edit([Bind(Include = "ID,Ime,Prezime,Mjesto,Zanimanje,SlikaPath,SlikaFile")] Djelatnik djelatnik, int[] SkolaID)
        {
            if (ModelState.IsValid)
            {
                Djelatnik dj = db.Djelatnik.Single(x => x.ID == djelatnik.ID);
                if (djelatnik.SlikaFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(djelatnik.SlikaFile.FileName) + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(djelatnik.SlikaFile.FileName);
                    djelatnik.SlikaPath = "~/Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    djelatnik.SlikaFile.SaveAs(fileName);
                }
                else
                {
                    djelatnik.SlikaPath = dj.SlikaPath;
                }
                
                if (System.IO.File.Exists(Request.MapPath(dj.SlikaPath)))
                {
                    System.IO.File.Delete(Request.MapPath(dj.SlikaPath));
                }
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
                db.Djelatnik.AddOrUpdate(djelatnik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(djelatnik);
        }

        [Authorize(Roles = "Admin")]
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
            return RedirectToAction("Index");
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