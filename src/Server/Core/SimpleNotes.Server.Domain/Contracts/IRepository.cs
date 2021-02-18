using SimpleNotes.Server.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleNotes.Server.Domain.Contracts
{
    public interface IRepository<TEntity>
        where TEntity : EntityBase
    {
        IQueryable<TEntity> GetEntities();

        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
