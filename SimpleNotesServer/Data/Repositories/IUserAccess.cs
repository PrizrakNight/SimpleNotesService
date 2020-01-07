using System;
using System.Collections.Generic;
using SimpleNotesServer.Data.Models;
using SimpleNotesServer.Data.Models.Users;

namespace SimpleNotesServer.Data.Repositories
{
    public interface IUserAccess<TUser> where TUser : EntityBase, IServerUser
    {
        IEnumerable<TUser> GetAllUsers();

        TUser GetUserBy(Func<TUser, bool> predicate);

        TUser GetUserByKey(int key);
    }
}