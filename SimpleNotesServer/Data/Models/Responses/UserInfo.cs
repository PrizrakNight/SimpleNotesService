using System.Collections.Generic;
using System.Linq;
using SimpleNotesServer.Data.Models.Notes;
using SimpleNotesServer.Data.Models.Users;

namespace SimpleNotesServer.Data.Models.Responses
{
    public struct UserInfo
    {
        public readonly string User_Name;

        public readonly string User_Role;

        public readonly SimpleUserOptions User_Options;

        public readonly long RegistrationDate;

        public readonly int Notes_Count;

        public readonly IEnumerable<SimpleNote> Notes;

        public UserInfo(SimpleUser user)
        {
            User_Name = user.Name;
            User_Role = user.Role;
            User_Options = user.Options;

            RegistrationDate = user.RegistrationDate;

            Notes = user.Notes;
            Notes_Count = Notes.Count();
        }
    }
}