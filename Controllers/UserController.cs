using SkolaProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SkolaProjekt.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        SkolaDBContext db = new SkolaDBContext();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Membership membership)
        {
            bool IsFound = db.Membership.Any(x => x.Username == membership.Username && x.Password == membership.Password);
            if (IsFound)
            {
                FormsAuthentication.SetAuthCookie(membership.Username, false);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Krivo korisničko ime ili lozinka!");
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(Models.Membership membership)
        {
            db.Membership.Add(membership);
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}