using System.Collections.Generic;
using SimpleNotesServer.Data.Models.Notes;
using SimpleNotesServer.Data.Models.Users;
using SimpleNotesServer.Data.Repositories;

namespace SimpleNotesServer.Extensions.Repository
{
    public static class SimpleRepositoryFluent
    {
        public static SimpleServerRepository AddUser(this SimpleServerRepository repository, SimpleUser user)
        {
            repository.Context.Users.Add(user);

            return repository;
        }

        public static SimpleServerRepository AddUsers(this SimpleServerRepository repository,
            IEnumerable<SimpleUser> users)
        {
            repository.Context.Users.AddRange(users);

            return repository;
        }

        public static SimpleServerRepository AddNote(this SimpleServerRepository repository, SimpleNote note)
        {
            repository.Context.Notes.Add(note);

            return repository;
        }

        public static SimpleServerRepository AddNotes(this SimpleServerRepository repository,
            IEnumerable<SimpleNote> notes)
        {
            repository.Context.Notes.AddRange(notes);

            return repository;
        }
    }
}