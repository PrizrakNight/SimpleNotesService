using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using SimpleNotesClient.Settings;

namespace SimpleNotesClient.WebApiQueries
{
    public class DeleteQueryBase : WebApiQueryBase
    {
        public DeleteQueryBase(params string[] segments) : base(ApplicationSettings.UrlAPI, segments) { }

        public override async Task<HttpResponseMessage> ExecuteAsync() => await UrlFull.DeleteAsync();
    }
}