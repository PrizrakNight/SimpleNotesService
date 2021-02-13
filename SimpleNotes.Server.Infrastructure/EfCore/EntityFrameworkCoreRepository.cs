using Microsoft.EntityFrameworkCore;
using SimpleNotes.Server.Domain.Contracts;
using SimpleNotes.Server.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleNotes.Server.Infrastructure
{
    public class EntityFrameworkCoreRepository<TEntity> : IRepository<TEntity>
        where TEntity : EntityBase
    {
        private readonly EntityFrameworkDbContext _context;

        public EntityFrameworkCoreRepository(EntityFrameworkDbContext context)
        {
            _context = context;
        }

        public Task DeleteAsync(TEntity entity)
        {
            var set = _context.Set<TEntity>();
            var finded = set.Find(entity.Key);

            if (finded != default)
                set.Remove(finded);

            return Task.CompletedTask;
        }

        public IQueryable<TEntity> GetEntities()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);

            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            var set = _context.Set<TEntity>();
            var finded = set.Find(entity.Key);

            if (finded != default)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }

            return Task.FromResult(entity);
        }
    }
}
