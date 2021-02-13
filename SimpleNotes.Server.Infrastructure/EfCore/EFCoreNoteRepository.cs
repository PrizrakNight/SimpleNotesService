using SimpleNotes.Server.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SimpleNotes.Server.Infrastructure
{
    public class EFCoreNoteRepository : EFCoreRepository<SimpleNote>
    {
        public EFCoreNoteRepository(EFCoreDbContext context) : base(context)
        {
        }

        public override Task<SimpleNote> UpdateAsync(SimpleNote entity)
        {
            var findedNote = context.Notes.Find(entity.Key);

            findedNote.Changed = DateTimeOffset.Now.ToUnixTimeSeconds();
            findedNote.Name = entity.Name;
            findedNote.Content = entity.Content;

            return Task.FromResult(findedNote);
        }
    }
}
