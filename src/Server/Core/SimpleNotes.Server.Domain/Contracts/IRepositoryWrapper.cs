using SimpleNotes.Server.Domain.Entities;
using System.Threading.Tasks;

namespace SimpleNotes.Server.Domain.Contracts
{
    public interface IRepositoryWrapper
    {
        IRepository<SimpleNote> Notes { get; }
        IRepository<SimpleUser> Users { get; }

        Task<int> SaveAsync();
    }
}
