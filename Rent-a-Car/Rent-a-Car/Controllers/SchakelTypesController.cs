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
    public class SchakelTypesController : Controller
    {
        private Entities db = new Entities();

        // GET: SchakelTypes
        public ActionResult Index()
        {
            return View(db.SchakelType.ToList());
        }

        // GET: SchakelTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchakelType schakelType = db.SchakelType.Find(id);
            if (schakelType == null)
            {
                return HttpNotFound();
            }
            return View(schakelType);
        }

        // GET: SchakelTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchakelTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Schakeltype1")] SchakelType schakelType)
        {
            if (ModelState.IsValid)
            {
                db.SchakelType.Add(schakelType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schakelType);
        }

        // GET: SchakelTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchakelType schakelType = db.SchakelType.Find(id);
            if (schakelType == null)
            {
                return HttpNotFound();
            }
            return View(schakelType);
        }

        // POST: SchakelTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Schakeltype1")] SchakelType schakelType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schakelType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schakelType);
        }

        // GET: SchakelTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchakelType schakelType = db.SchakelType.Find(id);
            if (schakelType == null)
            {
                return HttpNotFound();
            }
            return View(schakelType);
        }

        // POST: SchakelTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchakelType schakelType = db.SchakelType.Find(id);
            db.SchakelType.Remove(schakelType);
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
