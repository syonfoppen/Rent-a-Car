using Rent_a_Car.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Rent_a_Car.Controllers
{
    [Authorize]

    public class ShoppingCardController : Controller
    {
        private Entities db = new Entities();



        // GET: ShoppingCard
        public ActionResult Index()
        {
            Dictionary<AutoType, int> filteredautoTypes = null;
            if (Session["ShoppingCard"] != null)
            {
                List<AutoType> autoTypeslist = (List<AutoType>)Session["ShoppingCard"];
                filteredautoTypes = autoTypeslist.GroupBy(t => t.ID).ToDictionary(t => t.First(), y => y.Count());


            };
            return View(filteredautoTypes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DateTime begindatum, DateTime eindatum)
        {
            Dictionary<AutoType, int> filteredautoTypes = null;
            //Kijk of de session waar de geselecteerde autos bestaat
            if (Session["ShoppingCard"] != null)
            {
                //type cast de session
                List<AutoType> autoTypeslist = (List<AutoType>)Session["ShoppingCard"];
                //Maak een list voor de winkelwagen pagina zelf
                filteredautoTypes = autoTypeslist.GroupBy(t => t.ID).ToDictionary(t => t.First(), y => y.Count());
                //bekijk of de geselecteerde begin datum niet later is dan de datum van vandaag
                if (begindatum >= DateTime.Now)
                {
                    //kijk of de eind datum niet eerder is dan de begin datum
                    if (begindatum < eindatum)
                    {
                        List<Auto> gesleceteerdeautos = new List<Auto>();
                        decimal prijs = 0;
                        TimeSpan verhuringlengte = eindatum - begindatum;

                        //kijk of de lijst van geslecteerde autos inhoud heeft
                        if (autoTypeslist.Count != 0)
                        {
                            foreach (AutoType item in autoTypeslist)
                            {
                                //breken de prijs van de huring
                                foreach (var auto in item.Auto)
                                {
                                    //kijk of de auto beschikbaar is zo ja voeg deze toe aan de lijst van autos
                                    if ((db.Verhuring.Where(t => (t.Geldig == true) && (t.StartDatum <= eindatum) && (eindatum <= t.EindDatum) && (t.Auto.Where(d => d.AutoID == auto.AutoID).Count() > 0)).Count() == 0) && (gesleceteerdeautos.Where(t => t.AutoID == auto.AutoID).Count() == 0))
                                    {
                                        gesleceteerdeautos.Add(auto);
                                        break;
                                    }
                                }
                                //geef een error als er geen autos beschikbaar zijn van het auto type
                                if (gesleceteerdeautos.Where(t => t.AutoType.ID == item.ID).Count() == 0)
                                {
                                    ViewBag.Message = string.Format("Er zijn geen autos beschikbaar van het type {0} tussen {1} en {2}!", string.Format("{0} {1} - {2}", item.Merk, item.Type, item.Uitvoering), begindatum.ToString(), eindatum.ToString());
                                    return View(filteredautoTypes);
                                }
                            }
                           
                            if (gesleceteerdeautos.Count != 0)
                            {
                                List<Auto> gesleceteerdeautosReal = new List<Auto>();
                                foreach (var item in gesleceteerdeautos)
                                {
                                    gesleceteerdeautosReal.Add(db.Auto.Find(item.AutoID));
                                }
                                foreach (var item in filteredautoTypes.Keys)
                                {
                                    prijs += (decimal)db.spGetAutoPrice(begindatum, item.ID).First() * verhuringlengte.Days;
                                }

                                //maak een nieuwe verhuring aan
                                Verhuring newVerhuring = new Verhuring
                                {
                                    StartDatum = begindatum,
                                    EindDatum = eindatum,
                                    Auto = gesleceteerdeautosReal,
                                    GebruikerID = User.Identity.GetUserId(),
                                    Prijs = prijs,
                                    Geldig = true
                                };
                                db.Verhuring.Add(newVerhuring);
                                db.SaveChanges();
                                Session.Clear();
                                Session["Message"] = "Je verhuring is succesvol geplaatst!"; //MOET WORDEN VERANDERD
                                return RedirectToAction("index", "MijnVerhuringen");
                            }
                        }
                        ViewBag.Message = "Uw winkelmandje is leeg!";
                        return View(filteredautoTypes);
                    }
                    ViewBag.Message = "De Eind Datum kan niet eerder zijn dan de Begin Datum!";
                    return View(filteredautoTypes);
                }
                ViewBag.Message = "De begin Datum kan niet eerder zijn dan de datum van vandaag!";
                return View(filteredautoTypes);
            }

            //error als winkelmandje leeg is 
            ViewBag.Message = "Uw winkelmandje is leeg!";

            return View(filteredautoTypes);
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
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromCard(int? id)
        {
            if (Session["ShoppingCard"] != null)
            {
                List<AutoType> autoTypeslist = (List<AutoType>)Session["ShoppingCard"];

                foreach (var item in autoTypeslist)
                {
                    if (item.ID == id)
                    {
                        autoTypeslist.Remove(item);
                        break;
                    }
                }
                Session["ShoppingCard"] = autoTypeslist;

            }
            return RedirectToAction("Index");
        }
        public ActionResult RemoveAllFromCard(int? id)
        {
            if (Session["ShoppingCard"] != null)
            {
                List<AutoType> autoTypeslist = (List<AutoType>)Session["ShoppingCard"];
                autoTypeslist.RemoveAll(item => item.ID == id);
                Session["ShoppingCard"] = autoTypeslist;

            }
            return RedirectToAction("Index");
        }

        public ActionResult Emptycard()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}