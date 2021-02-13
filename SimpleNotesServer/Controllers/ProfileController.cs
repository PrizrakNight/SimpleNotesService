using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Services;
using System.Threading.Tasks;

namespace SimpleNotesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public ProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfileAsync()
        {
            return Ok(await _userProfileService.GetProfileAsync());
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] UserProfileRequest profileRequest)
        {
            await _userProfileService.UpdateProfileAsync(profileRequest);

            return Ok();
        }
    }
}
