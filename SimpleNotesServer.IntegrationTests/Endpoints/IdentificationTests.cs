﻿using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Models.Responses;
using SimpleNotesServer.IntegrationTests.Clients;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace SimpleNotesServer.IntegrationTests.Endpoints
{
    public class IdentificationTests : IClassFixture<ApplicationClient>
    {
        private readonly ApplicationClient _applicationClient;

        public IdentificationTests(ApplicationClient applicationClient)
        {
            _applicationClient = applicationClient;
        }

        [Fact]
        public async Task Post_RegisterReturnProfileResponse()
        {
            var request = new UserRegistrationRequest
            {
                AvatarUrl = "https://www.registration.com/userAvatar.png",
                Username = "New Registered User",
                Password = "Password_0dSwe",
                ConfirmPassword = "Password_0dSwe"
            };

            var jsonString = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _applicationClient.Client.PostAsync("/api/identification/registration", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var profile = JsonSerializer.Deserialize<UserProfileResponse>(responseJson);

            Assert.Equal(request.AvatarUrl, profile.AvatarUrl);
            Assert.Equal(request.Username, profile.Name);
            Assert.False(string.IsNullOrEmpty(profile.AccessToken));
        }
    }
}