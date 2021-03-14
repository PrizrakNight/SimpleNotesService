using Mapster;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UserProfileService> _logger;

        public UserProfileService(IUserAccessor userAccessor, IRepositoryWrapper repositoryWrapper, ILogger<UserProfileService> logger)
        {
            _userAccessor = userAccessor;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public Task<UserProfileResponse> GetProfileAsync()
        {
            _logger.LogInformation("Getting user profile with id '{userId}'", _userAccessor.CurrentUserId);

            var response = _userAccessor.CurrentUser.Adapt<UserProfileResponse>();

            return Task.FromResult(response);
        }

        public async Task<UserProfileResponse> UpdateProfileAsync(UserProfileRequest profileRequest)
        {
            _logger.LogInformation("Updating user profile to '{@profileRequest}'.", profileRequest);

            var currentUser = _userAccessor.CurrentUser;

            if (!string.IsNullOrEmpty(profileRequest.Username))
            {
                currentUser.Name = profileRequest.Username;

                _logger.LogDebug("Username changed");
            }

            if (!string.IsNullOrEmpty(profileRequest.AvatarUrl))
            {
                currentUser.AvatarUrl = profileRequest.AvatarUrl;

                _logger.LogDebug("AvatarUrl changed");
            }

            var updatedUser = await _repositoryWrapper.Users.UpdateAsync(currentUser);

            await _repositoryWrapper.SaveAsync();

            return updatedUser.Adapt<UserProfileResponse>();
        }
    }
}
