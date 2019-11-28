using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rent_a_Car.Models;

namespace Rent_a_Car.Controllers
{
    [Authorize(Roles = "Medewerker, Admin")]
    public class AdminController : Controller
    {
        private Entities db = new Entities();
        // GET: Admin
        public ActionResult Index()
        {
            decimal earningsmonth = 0;
            decimal earningsannual = 0;
            List<decimal> monthlyprovit = new List<decimal>();
            int totalrentsyear = db.Verhuring.Where(t => t.EindDatum.Year == DateTime.Now.Year).Count();
            foreach (var item in db.Verhuring.Where(t => t.EindDatum.Month == DateTime.Now.Month))
            {
                earningsmonth += item.Prijs;
            }
            foreach (var item in db.Verhuring.Where(t => t.EindDatum.Year == DateTime.Now.Year))
            {
                earningsannual += item.Prijs;
            }
            decimal total = 0;
            for (int i = 0; i < 12; i++)
            {
                foreach (var item in db.Verhuring.Where(t => t.EindDatum.Month == i))
                {
                    total += item.Prijs;
                }
                monthlyprovit.Add(total);
            }

            AdminViewModel model = new AdminViewModel
            {
                EarningsAnnual = earningsannual,
                Monthlyprovit = monthlyprovit,
                Earningsmonth = earningsmonth,
                TotalRentsYear = totalrentsyear
            };




            return View(model);
        }
    }
}