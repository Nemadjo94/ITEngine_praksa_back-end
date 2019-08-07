using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Praksa2.Repo.Models;

namespace Praksa2.Repo
{
    public class Context : IdentityDbContext<Users>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=.\\SQLEXPRESS;Database=Test2;Trusted_Connection=True;MultipleActiveResultSets=true");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
    
}
