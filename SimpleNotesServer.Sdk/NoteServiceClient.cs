using SimpleNotesServer.Sdk.Contracts;
using SimpleNotesServer.Sdk.Models.Identification;
using SimpleNotesServer.Sdk.Models.User;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SimpleNotesServer.Sdk
{
    public class NoteServiceClient : INoteServiceClient
    {
        public bool Authorized
        {
            get
            {
                return _httpClient.DefaultRequestHeaders.Authorization != default;
            }
        }

        public INoteClient NoteClient => throw new NotImplementedException();

        public IUserProfileClient ProfileClient => throw new NotImplementedException();

        private readonly HttpClient _httpClient;

        public NoteServiceClient(string baseApiUrl)
        {
            _httpClient = new HttpClient();

            _httpClient.BaseAddress = new Uri($"{baseApiUrl}/");
        }

        public async Task LoginAsync(string password, string username)
        {
            var model = new AuthorizationModel { Password = password, Username = username };
            var detailedUser = await _httpClient.SendObjectAsync<DetailedUserModel>("identification/authorization", "POST", model);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", detailedUser.AccessToken);

            ((UserProfileClient)ProfileClient).CurrentProfile = detailedUser;
        }

        public async Task LoginAsync(string jwt)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            await ProfileClient.GetProfileAsync();
        }

        public async Task RegisterAsync(RegistrationModel registrationModel)
        {
            var detailedUser = await _httpClient.SendObjectAsync<DetailedUserModel>("identification/registration", "POST", registrationModel);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", detailedUser.AccessToken);

            ((UserProfileClient)ProfileClient).CurrentProfile = detailedUser;
        }
    }
}
