using Microsoft.EntityFrameworkCore;
using SimpleNotes.Server.Domain.Contracts;
using SimpleNotes.Server.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleNotes.Server.Infrastructure
{
    public class EFCoreRepository<TEntity> : IRepository<TEntity>
        where TEntity : EntityBase
    {
        protected readonly EFCoreDbContext context;

        public EFCoreRepository(EFCoreDbContext context)
        {
            this.context = context;
        }

        public Task DeleteAsync(TEntity entity)
        {
            var set = context.Set<TEntity>();
            var finded = set.Find(entity.Key);

            if (finded != default)
                set.Remove(finded);

            return Task.CompletedTask;
        }

        public IQueryable<TEntity> GetEntities()
        {
            return context.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);

            return entity;
        }

        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);

            return Task.FromResult(entity);
        }
    }
}
