using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rent_a_Car.Models;

namespace Rent_a_Car.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index()
        {
            List<Verhuring> verhurings = db.Verhuring.ToList();
            List<AutoType> autos = new List<AutoType>();

            foreach (var item in verhurings)
            {
                foreach (var auto in item.Auto)
                {
                    autos.Add(auto.AutoType);
                }
            }

            Dictionary<AutoType, int> filteredautoTypes = autos.GroupBy(t => t.ID).ToDictionary(t => t.First(), y => y.Count());

            List<AutoType> model = filteredautoTypes.OrderByDescending(t => t.Value).Take(3).Select(t => t.Key).ToList();

            return View(model);
        }

    }
}