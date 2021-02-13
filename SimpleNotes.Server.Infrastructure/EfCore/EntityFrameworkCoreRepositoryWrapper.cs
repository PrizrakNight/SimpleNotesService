using SimpleNotes.Server.Domain.Contracts;
using SimpleNotes.Server.Domain.Entities;
using System.Threading.Tasks;

namespace SimpleNotes.Server.Infrastructure
{
    public class EntityFrameworkCoreRepositoryWrapper : IRepositoryWrapper
    {
        public IRepository<SimpleNote> Notes => _notes;

        public IRepository<SimpleUser> Users => _users;

        private readonly EntityFrameworkDbContext _context;
        private readonly EntityFrameworkCoreRepository<SimpleNote> _notes;
        private readonly EntityFrameworkCoreRepository<SimpleUser> _users;

        public EntityFrameworkCoreRepositoryWrapper(EntityFrameworkDbContext context)
        {
            _context = context;
            _notes = new EntityFrameworkCoreRepository<SimpleNote>(context);
            _users = new EntityFrameworkCoreRepository<SimpleUser>(context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
