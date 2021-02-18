using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

        public AuthorizationFilterAttribute(IPasswordHasher passwordHasher, IRepositoryWrapper repositoryWrapper)
        {
            _passwordHasher = passwordHasher;
            _repositoryWrapper = repositoryWrapper;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userRequest = context.ActionArguments.FirstOrDefault(arg => arg.Value is UserRequest).Value as UserRequest;

            if (userRequest != default)
            {
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
                }
            }
        }
    }
}
