using SimpleNotes.Server.Domain.Contracts;
using SimpleNotes.Server.Domain.Entities;
using System.Threading.Tasks;

namespace SimpleNotes.Server.Infrastructure
{
    public class EFCoreRepositoryWrapper : IRepositoryWrapper
    {
        public IRepository<SimpleNote> Notes => _notes;

        public IRepository<SimpleUser> Users => _users;

        private readonly EFCoreDbContext _context;
        private readonly EFCoreRepository<SimpleNote> _notes;
        private readonly EFCoreRepository<SimpleUser> _users;

        public EFCoreRepositoryWrapper(EFCoreDbContext context)
        {
            _context = context;
            _notes = new EFCoreNoteRepository(context);
            _users = new EFCoreRepository<SimpleUser>(context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
