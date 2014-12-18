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

        public DbSet<Line> Lines { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<LineStop> LineStops { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Stop> Stops { get; set; }
    }
}