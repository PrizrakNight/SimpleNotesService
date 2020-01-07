using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleNotesServer.Data.Repositories
{
    public abstract class BaseServerRepository<TContext> where TContext : DbContext, new()
    {
        public readonly TContext Context = new TContext();

        public void SaveChanges() => Context.SaveChanges();

        public Task<int> SaveChangesAsync() => Context.SaveChangesAsync();

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
            Context.SaveChangesAsync(cancellationToken);
    }
}