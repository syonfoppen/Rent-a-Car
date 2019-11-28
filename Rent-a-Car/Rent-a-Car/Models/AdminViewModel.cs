using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Rent_a_Car.Models
{
    public class AdminViewModel
    {
        public decimal Earningsmonth { get; set; }
        public decimal EarningsAnnual { get; set; }
        public int TotalRentsYear { get; set; }

        public List<decimal> Monthlyprovit { get; set; }

        public List<Verhuring> Verhurings { get; set; }
        public List<AutoType> TopAutoTypes { get; set; }

    }
}