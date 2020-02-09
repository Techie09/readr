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

        //Sql Connection
        private string _connection = null;

        public AppUserContext(string sqlConnection) : base()
        {
            _connection = sqlConnection;
        }

        //Tell DbContext what interface to use use with the connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //}


    }
}
