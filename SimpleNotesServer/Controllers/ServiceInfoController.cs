using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleNotesServer.Data.Contexts;
using SimpleNotesServer.Data.Repositories;

namespace SimpleNotesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceInfoController : RepositoryController<SimpleServerRepository, BaseServerContext>
    {
        [HttpGet("totalinfo")]
        public object GetTotalInfo() => new
        {
            total_registered = repository.Context.Users.Count(),
            total_notes = repository.Context.Notes.Count(),
            time_started = Program.TimeStarted
        };
    }
}
