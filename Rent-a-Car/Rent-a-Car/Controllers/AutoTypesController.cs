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
    public class AutoTypesController : Controller
    {
        private Entities db = new Entities();

        // GET: AutoTypes
        public ActionResult Index()
        {
            var autoType = db.AutoType.Include(a => a.Brandstof).Include(a => a.SchakelType);
            return View(autoType.ToList());
        }

        // GET: AutoTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoType autoType = db.AutoType.Find(id);
            if (autoType == null)
            {
                return HttpNotFound();
            }
            return View(autoType);
        }

        // GET: AutoTypes/Create
        public ActionResult Create()
        {
            ViewBag.BrandstofID = new SelectList(db.Brandstof, "ID", "BrandstofType");
            ViewBag.SchakelTypeID = new SelectList(db.SchakelType, "ID", "Schakeltype1");
            return View();
        }

        // POST: AutoTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Merk,Type,LaadRuimte,SchakelTypeID,Trekhaak,ZitPlaatsen,BrandstofID,Gewicht,AantalDeuren,Uitvoering,Beschikbaar")] AutoType autoType)
        {
            if (ModelState.IsValid)
            {
                db.AutoType.Add(autoType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandstofID = new SelectList(db.Brandstof, "ID", "BrandstofType", autoType.BrandstofID);
            ViewBag.SchakelTypeID = new SelectList(db.SchakelType, "ID", "Schakeltype1", autoType.SchakelTypeID);
            return View(autoType);
        }

        // GET: AutoTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoType autoType = db.AutoType.Find(id);
            if (autoType == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandstofID = new SelectList(db.Brandstof, "ID", "BrandstofType", autoType.BrandstofID);
            ViewBag.SchakelTypeID = new SelectList(db.SchakelType, "ID", "Schakeltype1", autoType.SchakelTypeID);
            return View(autoType);
        }

        // POST: AutoTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Merk,Type,LaadRuimte,SchakelTypeID,Trekhaak,ZitPlaatsen,BrandstofID,Gewicht,AantalDeuren,Uitvoering,Beschikbaar")] AutoType autoType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autoType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandstofID = new SelectList(db.Brandstof, "ID", "BrandstofType", autoType.BrandstofID);
            ViewBag.SchakelTypeID = new SelectList(db.SchakelType, "ID", "Schakeltype1", autoType.SchakelTypeID);
            return View(autoType);
        }

        // GET: AutoTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoType autoType = db.AutoType.Find(id);
            if (autoType == null)
            {
                return HttpNotFound();
            }
            return View(autoType);
        }

        // POST: AutoTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AutoType autoType = db.AutoType.Find(id);
            db.AutoType.Remove(autoType);
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
