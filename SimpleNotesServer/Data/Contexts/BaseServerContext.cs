using Microsoft.EntityFrameworkCore;
using SimpleNotesServer.Data.Models.Notes;
using SimpleNotesServer.Data.Models.Users;

namespace SimpleNotesServer.Data.Contexts
{
    public class BaseServerContext : DbContext
    {
        public DbSet<SimpleUser> Users { get; set; }

        public DbSet<SimpleNote> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("SimpleNotesDatabase");
            optionsBuilder.UseInMemoryDatabase("SimpleNotesDatabase");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SimpleUser>().HasOne(user => user.Options).WithOne(options => options.OwnerOptions)
                .HasForeignKey<SimpleUserOptions>(options => options.OwnerKey);
        }
    }
}