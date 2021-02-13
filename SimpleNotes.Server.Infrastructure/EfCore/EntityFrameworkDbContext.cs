using Microsoft.EntityFrameworkCore;
using SimpleNotes.Server.Domain.Entities;

namespace SimpleNotes.Server.Infrastructure
{
    public class EntityFrameworkDbContext : DbContext
    {
        public DbSet<SimpleUser> Users { get; set; }
        public DbSet<SimpleNote> Notes { get; set; }


        public EntityFrameworkDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
