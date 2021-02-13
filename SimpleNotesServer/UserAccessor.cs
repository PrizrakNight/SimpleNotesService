using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpleNotes.Server.Application;
using SimpleNotes.Server.Domain.Contracts;
using SimpleNotes.Server.Domain.Entities;
using System.Linq;
using System.Security.Claims;

namespace SimpleNotesServer
{
    internal class UserAccessor : IUserAccessor
    {
        public SimpleUser CurrentUser
        {
            get
            {
                var findedUser = _repositoryWrapper.Users
                    .GetEntities()
                    .Include(user => user.Notes)
                    .AsNoTracking()
                    .First(user => user.Key == CurrentUserId);

                return findedUser;
            }
        }

        public int CurrentUserId
        {
            get
            {
                var claimForKey = _httpContextAccessor.HttpContext.User.FindFirst(ClaimValueTypes.Integer32);

                return int.Parse(claimForKey.Value);
            }
        }

        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor, IRepositoryWrapper repositoryWrapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _repositoryWrapper = repositoryWrapper;
        }
    }
}
