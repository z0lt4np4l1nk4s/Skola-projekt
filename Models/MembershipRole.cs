using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkolaProjekt.Models
{
    public class MembershipRole
    {
        [Key]
        public int ID { get; set; }
        public Membership UserID { get; set; }
        public string Role { get; set; }
    }
}