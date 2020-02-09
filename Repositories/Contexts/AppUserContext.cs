using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Readr.DataObjects;

namespace Readr.Repositories.Contexts
{
    public class AppUserContext : DbContext
    {
        //Entities
        public DbSet<AppUser> AppUsers { get; set; }

        public AppUserContext(DbContextOptions<AppUserContext> options) : base(options)
        {
        }

        //Tell DbContext what interface to use use with the connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
