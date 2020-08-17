using Demo3._1.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Demo3._1.Data
{
    public class DemoContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Demo;Integrated Security=True;");

        }

        public DbSet<Player> Players { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Club>  Clubs { get; set; }
       
    }
}
