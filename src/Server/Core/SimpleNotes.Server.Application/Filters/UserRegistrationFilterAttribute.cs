using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

        public UserRegistrationFilterAttribute(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var registrationRequest = (UserRegistrationRequest)context.ActionArguments.FirstOrDefault(arg => arg.Value is UserRegistrationRequest).Value;

            if (registrationRequest != default)
            {
                var users = _repositoryWrapper.Users.GetEntities();

                if (users.Any(user => user.Name.Equals(registrationRequest.Username, StringComparison.OrdinalIgnoreCase)))
                {
                    context.Result = new BadRequestObjectResult(new BadResponse
                    {
                        StatusCode = 401,
                        Message = "A user with this name is already registered."
                    });
                }
            }
        }
    }
}
