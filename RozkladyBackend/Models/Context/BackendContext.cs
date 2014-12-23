using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RozkladyBackend.Models.Context
{
    public class BackendContext : IdentityDbContext<ApplicationUser>
    {
        public static BackendContext Create()
        {
            return new BackendContext();
        }

        public BackendContext() : base("BackendContext") { }

        public DbSet<Variant> Variants { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<VariantStop> VariantStops { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Stop> Stops { get; set; }
    }
}