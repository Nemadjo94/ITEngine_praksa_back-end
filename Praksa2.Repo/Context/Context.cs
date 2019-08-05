using Microsoft.EntityFrameworkCore;
using Praksa2.Repo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Praksa2.Repo
{
    public class Context : DbContext
    {
        public Context() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-VQT6JL4\SQLEXPRESS;Database=Test;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SuperAdmin>().HasData(
                new { ID = 1, Username = "Admin", Password = "admin123"}
            );
        }

        // Entities
        public DbSet<SuperAdmin> SuperAdmins { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
