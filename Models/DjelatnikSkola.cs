using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkolaProjekt.Models
{
    public class DjelatnikSkola
    {
        [Key]
        public int ID { get; set; }
        public int IDDjelatnik { get; set; }
        public int IDSkola { get; set; }

        public virtual ICollection<Skola> Skola { get; set; }
        public virtual ICollection<Djelatnik> Djelatnik { get; set; }
    }
}