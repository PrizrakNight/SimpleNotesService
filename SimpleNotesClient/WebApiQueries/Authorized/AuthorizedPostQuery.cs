﻿using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using SimpleNotesClient.Managers;

namespace SimpleNotesClient.WebApiQueries.Authorized
{
    public class AuthorizedPostQuery : PostQueryBase
    {
        public AuthorizedPostQuery(params string[] segments) : base(segments) { }

        public override async Task<HttpResponseMessage> ExecuteAsync() => await new Url(UrlFull)
            .WithOAuthBearerToken(AuthManager.AutharizationSuccess.Access_Token).PostJsonAsync(PostData);
    }
}