using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleNotesServer.Data.Contexts;
using SimpleNotesServer.Data.Models.Users;
using SimpleNotesServer.Data.Repositories;
using SimpleNotesServer.Extensions.Repository;

namespace SimpleNotesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : RepositoryController<SimpleServerRepository, BaseServerContext>
    {
        [Authorize]
        [HttpGet("username")]
        public string GetName() => repository.GetUserByClaimsPrincipal(User).Name;

        [Authorize]
        [HttpGet("options")]
        public SimpleUserOptions GetOptions() => repository.GetUserByClaimsPrincipal(User).Options;

        [Authorize]
        [HttpPut("options/changeavatar")]
        public IActionResult ChangeAvatar([FromQuery] string avatar)
        {
            repository.GetUserByClaimsPrincipal(User).Options.AvatarURL = avatar;
            repository.SaveChangesAsync();

            return Ok();
        }
    }
}
