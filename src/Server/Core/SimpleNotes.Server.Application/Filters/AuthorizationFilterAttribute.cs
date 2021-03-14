using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Models.Responses;
using SimpleNotes.Server.Domain.Contracts;
using System.Linq;

namespace SimpleNotes.Server.Application.Filters
{
    public class AuthorizationFilterAttribute : ActionFilterAttribute
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<AuthorizationFilterAttribute> _logger;

        public AuthorizationFilterAttribute(IPasswordHasher passwordHasher,
            IRepositoryWrapper repositoryWrapper,
            ILogger<AuthorizationFilterAttribute> logger)
        {
            _passwordHasher = passwordHasher;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userRequest = context.ActionArguments.FirstOrDefault(arg => arg.Value is UserRequest).Value as UserRequest;

            if (userRequest != default)
            {
                _logger.LogInformation("Checking user data for existence in the system. '{@userRequest}'", userRequest);

                var findedUser = _repositoryWrapper.Users.GetEntities()
                    .ToArray()
                    .FirstOrDefault(user => _passwordHasher.ComparePassword(userRequest.Password, user.PasswordHash));

                if (findedUser == default)
                {
                    context.Result = new BadRequestObjectResult(new BadResponse
                    {
                        StatusCode = 401,
                        Message = "Wrong login or password"
                    });

                    _logger.LogWarning("User entered invalid user data '{@userRequest}'", userRequest);
                }
            }
            else _logger.LogError($"The request could not find a parameter of the '{nameof(UserRequest)}' type, the parameter may be missing in the arguments of the controller method");
        }
    }
}
