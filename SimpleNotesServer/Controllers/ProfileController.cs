using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleNotes.Server.Application.Filters;
using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Models.Responses;
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
        [ProducesResponseType(typeof(BadResponse), 401)]
        [ProducesResponseType(typeof(UserProfileResponse), 200)]
        public async Task<IActionResult> GetProfileAsync()
        {
            return Ok(await _userProfileService.GetProfileAsync());
        }

        [HttpPatch]
        [ProducesResponseType(typeof(BadResponse), 401)]
        [ProducesResponseType(typeof(BadResponse), 400)]
        [ServiceFilter(typeof(UsernameMatchFilterAttribute))]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] UserProfileRequest profileRequest)
        {
            await _userProfileService.UpdateProfileAsync(profileRequest);

            return Ok();
        }
    }
}
