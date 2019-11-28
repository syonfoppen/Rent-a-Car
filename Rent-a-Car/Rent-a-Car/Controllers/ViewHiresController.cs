using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rent_a_Car.Models;

namespace Rent_a_Car.Controllers
{
    [Authorize]
    public class ViewHiresController : Controller
    {
        private Entities db = new Entities();

        // GET: ViewHires
        public ActionResult Index()
        {
            return View(db.Verhuring.ToList());
        }

        public void Cancel(int id)
        {
            Verhuring hire = db.Verhuring.Find(id);
            hire.Geldig = false;
            db.Entry(hire).State = EntityState.Modified;
            db.SaveChanges();
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
