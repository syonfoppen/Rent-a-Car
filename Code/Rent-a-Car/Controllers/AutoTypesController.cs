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

    public class AutoTypesController : Controller
    {
        private Entities db = new Entities();

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Merk,Type,LaadRuimte,SchakelTypeID,Trekhaak,ZitPlaatsen,BrandstofID,Gewicht,AantalDeuren,Uitvoering,Beschikbaar,Foto,price,upload")] AutoType autoType, HttpPostedFileBase upload, decimal? price)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    using var reader = new System.IO.BinaryReader(upload.InputStream);
                    autoType.Foto = reader.ReadBytes(upload.ContentLength);
                }


                db.AutoType.Add(autoType);
                db.SaveChanges();

                AutoPrijs autoPrijs = new AutoPrijs
                {
                    StartDatum = DateTime.Now,
                    Prijs = Convert.ToDecimal(price),
                    AutoTypeID = (from n in db.AutoType orderby n.ID descending select n.ID).First()
                };
                db.AutoPrijs.Add(autoPrijs);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.BrandstofID = new SelectList(db.Brandstof, "ID", "BrandstofType", autoType.BrandstofID);
            ViewBag.SchakelTypeID = new SelectList(db.SchakelType, "ID", "Schakeltype1", autoType.SchakelTypeID);
            return View(autoType);
        }

        // GET: AutoTypes/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID,Merk,Type,LaadRuimte,SchakelTypeID,Trekhaak,ZitPlaatsen,BrandstofID,Gewicht,AantalDeuren,Uitvoering,Beschikbaar, Foto")] AutoType autoType, HttpPostedFileBase upload, decimal? price)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    using var reader = new System.IO.BinaryReader(upload.InputStream);
                    autoType.Foto = reader.ReadBytes(upload.ContentLength);
                }

                if (price != null)
                {
                    AutoPrijs oldlodgeprice = db.AutoPrijs.Where(t => t.AutoTypeID == autoType.ID && t.EindDatum == null).First();

                    oldlodgeprice.EindDatum = DateTime.Now;
                    db.Entry(oldlodgeprice).State = EntityState.Modified;

                    AutoPrijs lodgePrice = new AutoPrijs
                    {
                        StartDatum = DateTime.Now,
                        Prijs = (decimal)price,
                        AutoTypeID = autoType.ID
                    };

                    db.AutoPrijs.Add(lodgePrice);
                }


                db.Entry(autoType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandstofID = new SelectList(db.Brandstof, "ID", "BrandstofType", autoType.BrandstofID);
            ViewBag.SchakelTypeID = new SelectList(db.SchakelType, "ID", "Schakeltype1", autoType.SchakelTypeID);
            return View(autoType);
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
