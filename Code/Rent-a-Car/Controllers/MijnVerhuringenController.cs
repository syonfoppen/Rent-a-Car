using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rent_a_Car.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
namespace Rent_a_Car.Controllers
{
    [Authorize]
    public class MijnVerhuringenController : Controller
    {
        private Entities db = new Entities();

        // GET: MijnVerhuringen
        public ActionResult Index()
        {
            string userid = User.Identity.GetUserId();
            var verhuring = db.Verhuring.Where(t => t.AspNetUsers.Id == userid);
            if (User.IsInRole("Admin") || User.IsInRole("Medewerker"))
            {
                verhuring = db.Verhuring;
            }


            return View(verhuring.ToList());
        }

        // GET: MijnVerhuringen/Details/5
        public ActionResult Details(int? id)
        {
            string userid = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verhuring verhuring = db.Verhuring.Find(id);
            if (verhuring == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("Admin") || User.IsInRole("Medewerker"))
            {
                return View(verhuring);
            }
            else if (verhuring.AspNetUsers.Id != userid)
            {
                return HttpNotFound();
            }


            return View(verhuring);
        }


        public ActionResult Print(int? id)
        {
            string userid = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verhuring verhuring = db.Verhuring.Find(id);
            if (verhuring == null)
            {
                return HttpNotFound();
            }
            if  (User.IsInRole("Admin") || User.IsInRole("Medewerker"))
            {
                return View(verhuring);
                
            }
            else if (verhuring.AspNetUsers.Id != userid)
            {
                return HttpNotFound();
            }

            return View(verhuring);
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
