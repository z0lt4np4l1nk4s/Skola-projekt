using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SkolaProjekt.Models
{
    [MetadataType(typeof(SkolaMetadata))]
    public partial class Skola
    {
    }
    public class SkolaMetadata
    {
        [Display(Name = "Web stranica")]
        public string WebStranica { get; set; }
    }
}