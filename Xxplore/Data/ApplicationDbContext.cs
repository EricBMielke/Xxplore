using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Xxplore.Models;

namespace Xxplore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Xplore.Models.UserProfile> UserProfile { get; set; }
        public DbSet<Xplore.Models.Country> Countries { get; set; }
        public DbSet<Xplore.Models.Highlight> Hightlights { get; set; }
        public DbSet<Xplore.Models.CountryVisited> CountriesVisited { get; set; }

    }
}
