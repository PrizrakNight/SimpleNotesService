using Microsoft.EntityFrameworkCore;
using SimpleNotes.Server.Domain.Entities;

namespace SimpleNotes.Server.Infrastructure
{
    public class EFCoreDbContext : DbContext
    {
        public DbSet<SimpleUser> Users { get; set; }
        public DbSet<SimpleNote> Notes { get; set; }


        public EFCoreDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
