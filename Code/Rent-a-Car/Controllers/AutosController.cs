﻿using System;
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
    [Authorize(Roles = "Admin")]
    public class AutosController : Controller
    {
        private Entities db = new Entities();

        // GET: Autos
        public ActionResult Index()
        {
            
            var auto = db.Auto.Include(a => a.AutoType);
            return View(auto.ToList());
        }



        // GET: Autos/Create
        public ActionResult Create()
        {
            ViewBag.AutoTypeID = db.AutoType.ToList();
            return View();
        }

        // POST: Autos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AutoID,AutoTypeID,Kenteken,BouwJaar,Beschikbaar")] Auto auto)
        {
            if (ModelState.IsValid)
            {
                db.Auto.Add(auto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }




            ViewBag.AutoTypeID = db.AutoType.ToList();
            return View(auto);
        }

        // GET: Autos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auto auto = db.Auto.Find(id);
            if (auto == null)
            {
                return HttpNotFound();
            }
            ViewBag.AutoType = db.AutoType.ToList() ;
            ViewBag.Id = id;
            return View(auto);
        }

        // POST: Autos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AutoID,AutoTypeID,Kenteken,BouwJaar,Beschikbaar")] Auto auto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AutoTypeID = new SelectList(db.AutoType, "ID", "Merk", auto.AutoTypeID);
            return View(auto);
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
