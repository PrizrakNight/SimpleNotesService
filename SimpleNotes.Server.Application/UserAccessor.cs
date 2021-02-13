using Microsoft.AspNetCore.Http;
using SimpleNotes.Server.Domain.Contracts;
using SimpleNotes.Server.Domain.Entities;
using System.Linq;
using System.Security.Claims;

namespace SimpleNotes.Server.Application
{
    internal class UserAccessor : IUserAccessor
    {
        public SimpleUser CurrentUser
        {
            get
            {
                var findedUser = _repositoryWrapper.Users.GetEntities().First(user => user.KeyEqualTo(CurrentUserId));

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
