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
    [ValidationFilter]
    public class IdentificationController : ControllerBase
    {
        private readonly IUserIdentificationService _userProfileService;

        public IdentificationController(IUserIdentificationService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpPost("registration")]
        [ServiceFilter(typeof(UserRegistrationFilterAttribute))]
        [ProducesResponseType(typeof(BadResponse), 401)]
        [ProducesResponseType(typeof(UserProfileResponse), 200)]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegistrationRequest registrationRequest)
        {
            var response = await _userProfileService.RegisterAsync(registrationRequest);

            return Ok(response);
        }

        [HttpPost("authorization")]
        [ServiceFilter(typeof(AuthorizationFilterAttribute))]
        [ProducesResponseType(typeof(BadResponse), 401)]
        [ProducesResponseType(typeof(UserProfileResponse), 200)]
        public async Task<IActionResult> AuthorizeUserAsync([FromBody] UserRequest userRequest)
        {
            var response = await _userProfileService.AuthorizeAsync(userRequest);

            return Ok(response);
        }
    }
}
