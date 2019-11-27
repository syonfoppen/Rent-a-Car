//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rent_a_Car.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AutoType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AutoType()
        {
            this.Auto = new HashSet<Auto>();
            this.AutoPrijs = new HashSet<AutoPrijs>();
        }
    
        public int ID { get; set; }
        public string Merk { get; set; }
        public string Type { get; set; }
        public int LaadRuimte { get; set; }
        public int SchakelTypeID { get; set; }
        public bool Trekhaak { get; set; }
        public int ZitPlaatsen { get; set; }
        public int BrandstofID { get; set; }
        public int Gewicht { get; set; }
        public int AantalDeuren { get; set; }
        public string Uitvoering { get; set; }
        public bool Beschikbaar { get; set; }
        public byte[] Foto { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Auto> Auto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AutoPrijs> AutoPrijs { get; set; }
        public virtual Brandstof Brandstof { get; set; }
        public virtual SchakelType SchakelType { get; set; }
    }
}
