using System;
using System.Collections.Generic;
using System.Text;
using KlinikaVeterinareTI2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KlinikaVeterinareTI2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Visit> Visits { get; set; }
    }
}
