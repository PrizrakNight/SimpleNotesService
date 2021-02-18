using Mapster;
using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Models.Responses;
using SimpleNotes.Server.Domain.Contracts;
using System.Threading.Tasks;

namespace SimpleNotes.Server.Application.Services.Implementation
{
    internal class UserProfileService : IUserProfileService
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UserProfileService(IUserAccessor userAccessor, IRepositoryWrapper repositoryWrapper)
        {
            _userAccessor = userAccessor;
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<UserProfileResponse> GetProfileAsync()
        {
            var response = _userAccessor.CurrentUser.Adapt<UserProfileResponse>();

            return Task.FromResult(response);
        }

        public async Task<UserProfileResponse> UpdateProfileAsync(UserProfileRequest profileRequest)
        {
            var currentUser = _userAccessor.CurrentUser;

            if (!string.IsNullOrEmpty(profileRequest.Username))
                currentUser.Name = profileRequest.Username;

            if (!string.IsNullOrEmpty(profileRequest.AvatarUrl))
                currentUser.AvatarUrl = profileRequest.AvatarUrl;

            var updatedUser = await _repositoryWrapper.Users.UpdateAsync(currentUser);

            await _repositoryWrapper.SaveAsync();

            return updatedUser.Adapt<UserProfileResponse>();
        }
    }
}
