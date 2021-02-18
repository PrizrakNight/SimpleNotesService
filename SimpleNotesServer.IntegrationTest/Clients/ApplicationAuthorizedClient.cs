using Microsoft.Extensions.DependencyInjection;
using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Models.Responses;
using SimpleNotes.Server.Application.Services;
using System.Net.Http.Headers;

namespace SimpleNotesServer.IntegrationTest.Clients
{
    public class ApplicationAuthorizedClient : ApplicationClient
    {
        public UserProfileResponse UserProfile { get; private set; }

        public ApplicationAuthorizedClient()
        {
            UserProfile = IdentifyTestUser(GetRegistrationRequest(), Server.Host.Services.GetRequiredService<IUserIdentificationService>());
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserProfile.AccessToken);
        }

        protected virtual UserProfileResponse IdentifyTestUser(UserRegistrationRequest registrationRequest, IUserIdentificationService userIdentificationService)
        {
            return userIdentificationService.RegisterAsync(registrationRequest).Result;
        }

        protected virtual UserRegistrationRequest GetRegistrationRequest(string userRole = "User")
        {
            return new UserRegistrationRequest
            {
                AvatarUrl = "https://www.fake.com/fakeAvatar.png",
                Username = "Test User",
                Password = "Simple_01Passw0rd",
                ConfirmPassword = "Simple_01Passw0rd"
            };
        }
    }
}
