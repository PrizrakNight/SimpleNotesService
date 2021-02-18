using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Models.Responses;
using SimpleNotes.Server.Domain.Contracts;
using System.Linq;

namespace SimpleNotes.Server.Application.Filters
{
    public class UsernameMatchFilterAttribute : ActionFilterAttribute
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UsernameMatchFilterAttribute(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.ActionArguments.FirstOrDefault(arg => arg.Value is UserProfileRequest).Value as UserProfileRequest;

            if(request != default)
            {
                if(_repositoryWrapper.Users.GetEntities().Any(user => user.Name == request.Username))
                {
                    context.Result = new BadRequestObjectResult(new BadResponse
                    {
                        StatusCode = 400,
                        Message = "A user with the same name already exists"
                    });
                }    
            }
        }
    }
}
