using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using joloochusite.Model;
using joloochusite.Models;

namespace joloochusite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        #region DbSet
        public  DbSet<Car> Cars { get; set; }
        public  DbSet<District> Districts { get; set; }
        public  DbSet<Mark> Marks { get; set; }
        public  DbSet<Order> Orders { get; set; }
        public  DbSet<Point> Points { get; set; }
        public  DbSet<Region> Regions { get; set; }
        public  DbSet<TypeCar> TypeCars { get; set; }
        public  DbSet<AppUser> AppUsers { get; set; }
        public  DbSet<Village> Villages { get; set; }
        public  DbSet<ApplicationUser> ApplicationUsers { get; set; }

        #endregion
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}