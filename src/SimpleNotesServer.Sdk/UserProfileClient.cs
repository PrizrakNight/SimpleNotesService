using SimpleNotesServer.Sdk.Contracts;
using SimpleNotesServer.Sdk.Models.User;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleNotesServer.Sdk
{
    public class UserProfileClient : IUserProfileClient
    {
        public DetailedUserModel CurrentProfile { get; internal set; }

        private readonly HttpClient _httpClient;

        public UserProfileClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<byte[]> DownloadAvatarAsync()
        {
            using (var webClient = new WebClient())
            {
                return await webClient.DownloadDataTaskAsync(new Uri(CurrentProfile.AvatarUrl));
            }
        }

        public async Task<DetailedUserModel> GetProfileAsync()
        {
            CurrentProfile = await _httpClient.GetObjectAsync<DetailedUserModel>("profile");

            return CurrentProfile;
        }

        public async Task<DetailedUserModel> UpdateProfileAsync(UserModel userModel)
        {
            CurrentProfile = await _httpClient.SendObjectAsync<DetailedUserModel>("profile", "PATCH", userModel);

            return CurrentProfile;
        }
    }
}
