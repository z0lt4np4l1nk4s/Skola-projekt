//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkolaProjekt.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Djelatnik
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Mjesto { get; set; }
        public string Adresa { get; set; }
        public string Zanimanje { get; set; }
        public string Telefon { get; set; }
        public Nullable<System.DateTime> DatumRodenja { get; set; }
        public Nullable<int> SkolaID { get; set; }
    
        public virtual Skola tblSkola { get; set; }
    }
}
