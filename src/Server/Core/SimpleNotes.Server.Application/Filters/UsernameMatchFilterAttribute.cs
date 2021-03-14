using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Models.Responses;
using SimpleNotes.Server.Domain.Contracts;
using System.Linq;

namespace SimpleNotes.Server.Application.Filters
{
    public class UsernameMatchFilterAttribute : ActionFilterAttribute
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<UsernameMatchFilterAttribute> _logger;

        public UsernameMatchFilterAttribute(IRepositoryWrapper repositoryWrapper, ILogger<UsernameMatchFilterAttribute> logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.ActionArguments.FirstOrDefault(arg => arg.Value is UserProfileRequest).Value as UserProfileRequest;

            if (request != default)
            {
                if (_repositoryWrapper.Users.GetEntities().Any(user => user.Name == request.Username))
                {
                    context.Result = new BadRequestObjectResult(new BadResponse
                    {
                        StatusCode = 400,
                        Message = "A user with the same name already exists"
                    });

                    _logger.LogWarning("A user with the same username already exists on the system '{@request}'.", request);
                }
            }
            else _logger.LogError($"The request could not find a parameter of the '{nameof(UserProfileRequest)}' type, the parameter may be missing in the arguments of the controller method");
        }
    }
}
