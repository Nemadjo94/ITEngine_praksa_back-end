using Microsoft.EntityFrameworkCore;
using Praksa2.Repo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Praksa2.Repo
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-VQT6JL4\SQLEXPRESS;Database=Test;Trusted_Connection=True;");
        }
        // Seed super admin data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash("Admin123", out passwordHash, out passwordSalt);
            modelBuilder.Entity<Users>().HasData(
                new { ID = 1, Username = "Admin", PasswordHash  = passwordHash, PasswordSalt = passwordSalt }//Change data later
            );
        }

        // Entities
        public DbSet<Users> Users { get; set; }


        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // If password is null throw argument null exception
            if (password == null) throw new ArgumentNullException("password");
            // If password is whitespace throw argument exception
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

    }
    
}
