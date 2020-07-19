using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkolaProjekt.Models
{
    public class Membership
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage ="Korisničko ime je obavezno")]
        [Display(Name ="Korisničko ime")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna")]
        [Display(Name ="Lozinka")]
        public string Password { get; set; }
    }
}