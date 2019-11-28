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
    public class RentController : Controller
    {
        private Entities db = new Entities();

        // GET: Rent
        public ActionResult Index()
        {
            var autoTypes = db.AutoType;
            return View(autoTypes.Where(t => t.Beschikbaar == true && t.Auto.Where(a => a.Beschikbaar == true).Count() > 0).ToList());
        }
    }
}