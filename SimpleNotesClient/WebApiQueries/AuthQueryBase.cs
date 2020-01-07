using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SimpleNotesClient.Models.Authentication;

namespace SimpleNotesClient.WebApiQueries
{
    public class AuthQueryBase : PostQueryBase
    {
        public AuthQueryBase(string uName, string uPassword, string authSegment) : base("token", authSegment)
        {
            PostData = new
            {
                username = uName,
                password = uPassword
            };
        }

        public async Task<AutharizationSuccess> Authorize()
        {
            HttpResponseMessage response = await ExecuteAsync();

            JObject result = JObject.Parse(await response.Content.ReadAsStringAsync());

            AutharizationSuccess success = new AutharizationSuccess
            {
                Access_Token = (string) result["access_Token"],
                Username = (string) result["for_User"]
            };

            return success;
        }
    }
}