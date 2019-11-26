using Microsoft.AspNet.Identity;
using Rent_a_Car.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;

namespace Rent_a_Car.Controllers
{
    [Authorize]
    public class ProfileController : Controller
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

        // GET: Profile
        public ActionResult Index(string message = "", string messageColor = "primary")
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            ViewBag.user = user;
            ViewBag.profileMessage = message;
            ViewBag.profileMessageColor = messageColor;
            return View();
        }

        public ActionResult Apply(RegisterViewModel registerView)
        {
            var changedUser = UserManager.FindById(User.Identity.GetUserId());
            changedUser.Voornaam = registerView.Voornaam;
            changedUser.Tussenvoegsel = registerView.Tussenvoegsel;
            changedUser.Achternaam = registerView.Achternaam;
            changedUser.Geboortedatum = registerView.Geboortedatum;

            changedUser.PhoneNumber = registerView.PhoneNumber;
            changedUser.Email = registerView.Email;

            changedUser.Land = registerView.Land;
            changedUser.Provincie = registerView.Provincie;
            changedUser.Plaats = registerView.Plaats;
            changedUser.PostCode = registerView.PostCode;
            changedUser.Straat = registerView.Straat;
            changedUser.Huisnummer = registerView.Huisnummer;
            changedUser.Toevoeging = registerView.Toevoeging;



            if (!UserManager.Update(changedUser).Succeeded)
            {
                return RedirectToAction("Index", new { message = "Failed to change your information.", messageColor = "danger" });
            }
            
            if (registerView.ProfileNewPassword != null)
            {
                if (!UserManager.ChangePassword(changedUser.Id, registerView.ProfileCurrentPassword, registerView.ProfileNewPassword).Succeeded)
                {
                    return RedirectToAction("Index", new { message = "Failed to change your password.", messageColor = "danger" });
                }
            }

            return RedirectToAction("Index", new { message = "Succesfully changed your information.", messageColor = "success" });
        }
    }
}