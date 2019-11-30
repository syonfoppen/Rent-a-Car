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
    public class BrandstofController : Controller
    {
        private Entities db = new Entities();

        // GET: Brandstof
        public ActionResult Index()
        {
            return View(db.Brandstof.ToList());
        }


        // GET: Brandstof/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brandstof/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BrandstofType")] Brandstof brandstof)
        {
            if (ModelState.IsValid)
            {
                db.Brandstof.Add(brandstof);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brandstof);
        }

        // GET: Brandstof/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brandstof brandstof = db.Brandstof.Find(id);
            if (brandstof == null)
            {
                return HttpNotFound();
            }
            return View(brandstof);
        }

        // POST: Brandstof/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BrandstofType")] Brandstof brandstof)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brandstof).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brandstof);
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
