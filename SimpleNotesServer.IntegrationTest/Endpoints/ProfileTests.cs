using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Models.Responses;
using SimpleNotesServer.IntegrationTest.Clients;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace SimpleNotesServer.IntegrationTest.Endpoints
{
    public class ProfileTests : IClassFixture<ApplicationAuthorizedClient>
    {
        private readonly ApplicationAuthorizedClient _applicationAuthorizedClient;

        public ProfileTests(ApplicationAuthorizedClient applicationAuthorizedClient)
        {
            _applicationAuthorizedClient = applicationAuthorizedClient;
        }

        [Fact]
        public async Task Get_ShouldReturnProfile()
        {
            var responseMessage = await _applicationAuthorizedClient.Client.GetAsync("/api/profile");
            var responseJson = await responseMessage.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);

            var responseProfile = JsonSerializer.Deserialize<UserProfileResponse>(responseJson);

            Assert.Equal(_applicationAuthorizedClient.UserProfile.Role, responseProfile.Role);
        }

        [Fact]
        public async Task Update_ShouldUpdateProfile()
        {
            var request = new UserProfileRequest
            {
                Username = "Updated username",
                AvatarUrl = "https://www.updatedAvatar.com/avatar.svg"
            };

            var jsonString = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage
            {
                Content = content,
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri($"{_applicationAuthorizedClient.Client.BaseAddress}api/profile")
            };

            var response = await _applicationAuthorizedClient.Client.SendAsync(requestMessage);
            var responseJson = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var profile = JsonSerializer.Deserialize<UserProfileResponse>(responseJson);

            Assert.Equal(request.AvatarUrl, profile.AvatarUrl);
            Assert.Equal(request.Username, profile.Name);
        }
    }
}
