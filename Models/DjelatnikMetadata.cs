using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SkolaProjekt.Models
{
    [MetadataType(typeof(DjelatnikMetadata))]
    public partial class Djelatnik
    {
    }
    public class DjelatnikMetadata
    {
        [Required(ErrorMessage = "Vaše ime je obavezno!")]
        public string Ime { get; set; }
        
        [Required]
        public string Prezime { get; set; }
        
        [Required]
        public string Mjesto { get; set; }

        [Display(Name = "Datum rođenja")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:dd-MM-yyyy}")]
        public DateTime? DatumRodenja { get; set; }

        [Required(ErrorMessage = "Odaberite školu!")]
        [Display(Name = "Škola")]
        public int? SkolaID { get; set; }
    }
}