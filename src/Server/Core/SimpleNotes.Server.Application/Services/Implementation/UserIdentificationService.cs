using Mapster;
using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Models.Responses;
using SimpleNotes.Server.Domain;
using SimpleNotes.Server.Domain.Contracts;
using SimpleNotes.Server.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleNotes.Server.Application.Services.Implementation
{
    internal class UserIdentificationService : IUserIdentificationService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ITokenService _tokenService;

        public UserIdentificationService(IPasswordHasher passwordHasher, IRepositoryWrapper repositoryWrapper, ITokenService tokenService)
        {
            _passwordHasher = passwordHasher;
            _repositoryWrapper = repositoryWrapper;
            _tokenService = tokenService;
        }

        public async Task<UserProfileResponse> AuthorizeAsync(UserRequest userRequest)
        {
            var findedUser = _repositoryWrapper.Users.GetEntities().ToArray().First(user => _passwordHasher.ComparePassword(userRequest.Password, user.PasswordHash));
            var response = findedUser.Adapt<UserProfileResponse>();

            response.AccessToken = await _tokenService.GenerateTokenAsync(findedUser);

            return response;
        }

        public async Task<UserProfileResponse> RegisterAsync(UserRegistrationRequest userRequest)
        {
            var simpleUser = new SimpleUser
            {
                Name = userRequest.Username,
                AvatarUrl = userRequest.AvatarUrl,
                Role = ServerRoles.User
            };

            simpleUser.RegistrationDate = simpleUser.LastEntrance = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            simpleUser.PasswordHash = _passwordHasher.HashPassword(userRequest.Password);

            if (string.IsNullOrEmpty(userRequest.AvatarUrl))
                simpleUser.AvatarUrl = UserDefaults.DefaultAvatarUrl;

            await _repositoryWrapper.Users.InsertAsync(simpleUser);

            var response = simpleUser.Adapt<UserProfileResponse>();

            response.AccessToken = await _tokenService.GenerateTokenAsync(simpleUser);

            await _repositoryWrapper.SaveAsync();

            return response;
        }
    }
}
