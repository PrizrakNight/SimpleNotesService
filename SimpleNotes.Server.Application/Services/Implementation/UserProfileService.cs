using Mapster;
using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Models.Responses;
using SimpleNotes.Server.Domain.Contracts;
using SimpleNotes.Server.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleNotes.Server.Application.Services.Implementation
{
    internal class UserProfileService : IUserProfileService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ITokenService _tokenService;

        public UserProfileService(IPasswordHasher passwordHasher, IRepositoryWrapper repositoryWrapper, ITokenService tokenService)
        {
            _passwordHasher = passwordHasher;
            _repositoryWrapper = repositoryWrapper;
            _tokenService = tokenService;
        }

        public async Task<UserProfileResponse> AuthorizeAsync(UserRequest userRequest)
        {
            var passwordHash = _passwordHasher.HashPassword(userRequest.Password);
            var findedUser = _repositoryWrapper.Users.GetEntities().First(user => user.PasswordHash.Equals(passwordHash) && user.Name.Equals(userRequest.Username));
            var response = findedUser.Adapt<UserProfileResponse>();

            response.AccessToken = await _tokenService.GenerateTokenAsync(findedUser);

            return response;
        }

        public async Task<UserProfileResponse> RegisterAsync(UserRegistrationRequest userRequest)
        {
            var simpleUser = userRequest.Adapt<SimpleUser>();

            simpleUser.RegistrationDate = simpleUser.LastEntrance = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            simpleUser.PasswordHash = _passwordHasher.HashPassword(userRequest.Password);

            await _repositoryWrapper.Users.InsertAsync(simpleUser);

            var response = simpleUser.Adapt<UserProfileResponse>();

            response.AccessToken = await _tokenService.GenerateTokenAsync(simpleUser);

            return response;
        }
    }
}
