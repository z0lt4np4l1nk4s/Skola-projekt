using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SkolaProjekt.Models
{
    public class Djelatnik
    {
        [Key]
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Mjesto { get; set; }
        public string Zanimanje { get; set; }

        public virtual ICollection<DjelatnikSkola> DjelatnikSkola { get; set; }
    }
}