using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkolaProjekt.Models
{
    public class Skola
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Mjesto { get; set; }
        public string Adresa { get; set; }
        public string WebStranica { get; set; }

        public virtual ICollection<DjelatnikSkola> DjelatnikSkola { get; set; }
    }
}