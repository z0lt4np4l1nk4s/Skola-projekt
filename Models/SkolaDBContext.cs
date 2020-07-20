using SkolaProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SkolaProjekt.Models
{
    public class SkolaDBContext : DbContext
    {
        public DbSet<Djelatnik> Djelatnik { get; set; }
        public DbSet<DjelatnikSkola> DjelatnikSkola { get; set; }
        public DbSet<Skola> Skola { get; set; }
    }
}