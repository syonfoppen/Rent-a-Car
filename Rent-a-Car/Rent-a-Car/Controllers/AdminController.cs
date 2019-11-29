using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            int totalrentsyear = db.Verhuring.Where(t => t.StartDatum.Year == DateTime.Now.Year).Count();
            foreach (var item in db.Verhuring.Where(t => t.StartDatum.Month == DateTime.Now.Month))
            {
                earningsmonth += item.Prijs;
            }
            foreach (var item in db.Verhuring.Where(t => t.StartDatum.Year == DateTime.Now.Year))
            {
                earningsannual += item.Prijs;
            }

            for (int i = 0; i < 12; i++)
            {
                decimal total = 0;
                foreach (var item in db.Verhuring.Where(t => t.StartDatum.Month == i).OrderBy(t => t.StartDatum.Month))
                {
                    total += item.Prijs;
                }
                monthlyprovit.Add(total);
            }
                DateTime tomorrow = DateTime.Now.Date.AddDays(1.0);

            
            List<Verhuring> verhurings = db.Verhuring.Where(t => t.StartDatum == tomorrow).ToList();

            List<Verhuring> verhurings_all = db.Verhuring.ToList();
            List<AutoType> autos = new List<AutoType>();

            foreach (var item in verhurings_all)
            {
                foreach (var auto in item.Auto)
                {
                    autos.Add(auto.AutoType);
                }
            }
            Dictionary<AutoType, int> filteredautoTypes = autos.GroupBy(t => t.ID).ToDictionary(t => t.First(), y => y.Count());

            AdminViewModel model = new AdminViewModel
            {
                EarningsAnnual = earningsannual,
                Monthlyprovit = monthlyprovit,
                Earningsmonth = earningsmonth,
                TotalRentsYear = totalrentsyear,
                Verhurings = verhurings,
                TopAutoTypes = filteredautoTypes.OrderByDescending(t => t.Value).Take(10).Select(t => t.Key).ToList()
        };




            return View(model);
        }
    }
}