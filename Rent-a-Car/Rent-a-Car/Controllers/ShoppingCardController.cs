using Rent_a_Car.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rent_a_Car.Controllers
{
    public class ShoppingCardController : Controller
    {
        private Entities db = new Entities();
        // GET: ShoppingCard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddToCard(int? id)
        {
            AutoType auto = db.AutoType.Find(id);
            if (Session["ShoppingCard"] == null)
            {
                List<AutoType> autoTypeslist = new List<AutoType>();
                autoTypeslist.Add(auto);
                Session["ShoppingCard"] = autoTypeslist;
            }
            else
            {
                List<AutoType> autoTypeslist = (List<AutoType>)Session["ShoppingCard"];
                autoTypeslist.Add(auto);
                Session["ShoppingCard"] = autoTypeslist;
            }
            return RedirectToAction("Index", "Rent");
        }
    }
}