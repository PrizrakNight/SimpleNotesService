using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using SimpleNotesClient.Settings;

namespace SimpleNotesClient.WebApiQueries
{
    public class GetQueryBase : WebApiQueryBase
    {
        public bool UseToken { get; set; }

        public GetQueryBase(params string[] segments) : base(ApplicationSettings.UrlAPI, segments) { }

        public override async Task<HttpResponseMessage> ExecuteAsync() => await UrlFull.GetAsync();
    }
}