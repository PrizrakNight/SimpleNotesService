using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleNotesServer.Data.Contexts;
using SimpleNotesServer.Data.Models.Notes;
using SimpleNotesServer.Data.Models.Users;
using SimpleNotesServer.Data.Repositories;

namespace SimpleNotesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : RepositoryController<SimpleServerRepository, BaseServerContext>
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("allusers")]
        public IEnumerable<SimpleUser> GetAllUsers() => repository.GetAllUsers();

        [Authorize(Roles = "Admin")]
        [HttpGet("usernotes/{userKey}")]
        public IEnumerable<SimpleNote> GetUserNotes(int userKey) => repository.GetUserByKey(userKey).Notes;

        [Authorize(Roles = "Admin")]
        [HttpGet("useroptions/{userKey}")]
        public SimpleUserOptions GetUserOptions(int userKey) => repository.GetUserByKey(userKey).Options;
    }
}
