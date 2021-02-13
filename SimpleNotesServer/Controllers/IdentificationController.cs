using Microsoft.AspNetCore.Mvc;
using SimpleNotes.Server.Application.Filters;
using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Services;
using System.Threading.Tasks;

namespace SimpleNotesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidationFilter]
    public class IdentificationController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public IdentificationController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpPost("registration")]
        [ServiceFilter(typeof(UserRegistrationFilterAttribute))]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegistrationRequest registrationRequest)
        {
            var response = await _userProfileService.RegisterAsync(registrationRequest);

            return Ok(response);
        }

        [HttpPost("authorize")]
        [ServiceFilter(typeof(AuthorizationFilterAttribute))]
        public async Task<IActionResult> AuthorizeUserAsync([FromBody] UserRequest userRequest)
        {
            var response = await _userProfileService.AuthorizeAsync(userRequest);

            return Ok(response);
        }
    }
}
