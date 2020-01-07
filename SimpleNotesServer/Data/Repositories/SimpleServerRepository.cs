using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleNotesServer.Data.Contexts;
using SimpleNotesServer.Data.Models.Notes;
using SimpleNotesServer.Data.Models.Users;

namespace SimpleNotesServer.Data.Repositories
{
    public class SimpleServerRepository : BaseServerRepository<BaseServerContext>, IUserAccess<SimpleUser>,
        INoteAccess<SimpleNote>
    {
        public IEnumerable<SimpleNote> GetAllNotes() => Context.Notes;

        public IEnumerable<SimpleUser> GetAllUsers() => Context.Users;

        public SimpleNote GetNoteBy(Func<SimpleNote, bool> predicate) => Context.Notes.SingleOrDefault(predicate);

        public SimpleNote GetNoteByKey(int key) => Context.Notes.SingleOrDefault(note => note.KeyEqualTo(key));

        public SimpleUser GetUserBy(Func<SimpleUser, bool> predicate) => Context.Users
            .Include(user => user.Options)
            .Include(user => user.Notes)
            .SingleOrDefault(predicate);

        public SimpleUser GetUserByKey(int key) => GetUserBy(user => user.KeyEqualTo(key));
    }
}