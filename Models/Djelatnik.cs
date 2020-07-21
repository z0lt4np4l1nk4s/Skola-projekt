using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkolaProjekt.Models
{
    public partial class Djelatnik
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }
        public string Mjesto { get; set; }
        public string Zanimanje { get; set; }
        [Display(Name = "Slika")]
        public string SlikaPath { get; set; }
        [NotMapped]
        public HttpPostedFileBase SlikaFile { get; set; }

        public virtual ICollection<DjelatnikSkola> DjelatnikSkola { get; set; }
    }
}