using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Rent_a_Car.Models;

namespace Rent_a_Car.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagerController : Controller
    {
        private Entities db = new Entities();

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: UserManager
        public ActionResult Index()
        {
            return View(db.AspNetUsers.ToList());
        }

        // GET: UserManager/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers.LockoutEndDateUtc != null)
            {
                aspNetUsers.Lockout = true;
            }
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }


        // GET: UserManager/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers.LockoutEndDateUtc != null)
            {
                aspNetUsers.Lockout = true;
            }
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }

            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", aspNetUsers.AspNetRoles.First().Id);

            return View(aspNetUsers);
        }

        // POST: UserManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Voornaam,Tussenvoegsel,Achternaam,Geboortedatum,Straat,Huisnummer,Toevoeging,PostCode,Plaats,Provincie,Land,Email,EmailConfirmed,PhoneNumber,TwoFactorEnabled,UserName,Lockout")] AspNetUsers aspNetUsersData, string RoleId)
        {
            if (ModelState.IsValid)
            {
                var newAspNetUsers = db.AspNetUsers.Find(aspNetUsersData.Id);

                newAspNetUsers.Voornaam = aspNetUsersData.Voornaam;
                newAspNetUsers.Tussenvoegsel = aspNetUsersData.Tussenvoegsel;
                newAspNetUsers.Achternaam = aspNetUsersData.Achternaam;
                newAspNetUsers.Geboortedatum = aspNetUsersData.Geboortedatum;

                newAspNetUsers.PhoneNumber = aspNetUsersData.PhoneNumber;
                newAspNetUsers.Email = aspNetUsersData.Email;
                newAspNetUsers.EmailConfirmed = aspNetUsersData.EmailConfirmed;

                newAspNetUsers.TwoFactorEnabled = aspNetUsersData.TwoFactorEnabled;
                newAspNetUsers.UserName = aspNetUsersData.Email;

                newAspNetUsers.Land = aspNetUsersData.Land;
                newAspNetUsers.Provincie = aspNetUsersData.Provincie;
                newAspNetUsers.Plaats = aspNetUsersData.Plaats;
                newAspNetUsers.PostCode = aspNetUsersData.PostCode;
                newAspNetUsers.Straat = aspNetUsersData.Straat;
                newAspNetUsers.Huisnummer = aspNetUsersData.Huisnummer;
                newAspNetUsers.Toevoeging = aspNetUsersData.Toevoeging;

                if (aspNetUsersData.Lockout)
                {
                    newAspNetUsers.LockoutEndDateUtc = DateTime.MaxValue;
                }
                else
                {
                    newAspNetUsers.LockoutEndDateUtc = null;
                }

                UserManager.RemoveFromRole(newAspNetUsers.Id, newAspNetUsers.AspNetRoles.First().Name);
                UserManager.AddToRole(newAspNetUsers.Id, db.AspNetRoles.Find(RoleId).Name);

                db.Entry(newAspNetUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetUsersData);
        }

        // GET: UserManager/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // POST: UserManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUsers);
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
