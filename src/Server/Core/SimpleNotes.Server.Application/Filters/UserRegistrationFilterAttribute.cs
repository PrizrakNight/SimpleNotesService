using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Models.Responses;
using SimpleNotes.Server.Domain.Contracts;
using System;
using System.Linq;

namespace SimpleNotes.Server.Application.Filters
{
    public class UserRegistrationFilterAttribute : ActionFilterAttribute
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<UserRegistrationFilterAttribute> _logger;

        public UserRegistrationFilterAttribute(IRepositoryWrapper repositoryWrapper, ILogger<UserRegistrationFilterAttribute> logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var registrationRequest = (UserRegistrationRequest)context.ActionArguments.FirstOrDefault(arg => arg.Value is UserRegistrationRequest).Value;

            if (registrationRequest != default)
            {
                _logger.LogInformation("Check if a user is registered in the system with the '{username}'", registrationRequest.Username);

                var users = _repositoryWrapper.Users.GetEntities();

                if (users.Any(user => user.Name.Equals(registrationRequest.Username, StringComparison.OrdinalIgnoreCase)))
                {
                    context.Result = new BadRequestObjectResult(new BadResponse
                    {
                        StatusCode = 401,
                        Message = "A user with this name is already registered."
                    });

                    _logger.LogWarning("The user '{username}' is already registered in the system.", registrationRequest.Username);
                }
            }
            else _logger.LogError($"The request could not find a parameter of the '{nameof(UserRegistrationRequest)}' type, the parameter may be missing in the arguments of the controller method");
        }
    }
}
