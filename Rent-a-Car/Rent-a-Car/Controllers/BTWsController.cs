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
    public class BTWsController : Controller
    {
        private Entities db = new Entities();

        // GET: BTWs
        public ActionResult Index()
        {
            return View(db.BTW.ToList());
        }

        // GET: BTWs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BTW bTW = db.BTW.Find(id);
            if (bTW == null)
            {
                return HttpNotFound();
            }
            return View(bTW);
        }

        // GET: BTWs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BTWs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BTW1,StartDatum,EindDatum,ID")] BTW bTW)
        {
            if (ModelState.IsValid)
            {
                db.BTW.Add(bTW);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bTW);
        }

        // GET: BTWs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BTW bTW = db.BTW.Find(id);
            if (bTW == null)
            {
                return HttpNotFound();
            }
            return View(bTW);
        }

        // POST: BTWs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BTW1,StartDatum,EindDatum,ID")] BTW bTW)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bTW).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bTW);
        }

        // GET: BTWs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BTW bTW = db.BTW.Find(id);
            if (bTW == null)
            {
                return HttpNotFound();
            }
            return View(bTW);
        }

        // POST: BTWs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BTW bTW = db.BTW.Find(id);
            db.BTW.Remove(bTW);
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
