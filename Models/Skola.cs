using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkolaProjekt.Models
{
    public partial class Skola
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public string Mjesto { get; set; }
        public string Adresa { get; set; }
        [Display(Name ="Web stranica")]
        public string WebStranica { get; set; }

        public virtual ICollection<DjelatnikSkola> DjelatnikSkola { get; set; }
    }
}