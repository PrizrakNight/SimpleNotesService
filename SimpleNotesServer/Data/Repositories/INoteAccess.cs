using System;
using System.Collections.Generic;
using SimpleNotesServer.Data.Models;
using SimpleNotesServer.Data.Models.Notes;

namespace SimpleNotesServer.Data.Repositories
{
    public interface INoteAccess<TNote> where TNote : EntityBase, IServerNote
    {
        IEnumerable<TNote> GetAllNotes();

        TNote GetNoteBy(Func<TNote, bool> predicate);

        TNote GetNoteByKey(int key);
    }
}