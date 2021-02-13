using System.Net.Http;
using System.Threading.Tasks;
using SimpleNotesClient.Managers;

namespace SimpleNotesClient.WebApiQueries.Authorized
{
    public class AuthorizedGetQuery : GetQueryBase
    {
        public AuthorizedGetQuery(params string[] segments) : base(segments) { }

        public override async Task<HttpResponseMessage> ExecuteAsync() => await new Url(UrlFull)
            .WithOAuthBearerToken(AuthManager.AutharizationSuccess.Access_Token).GetAsync();
    }
}