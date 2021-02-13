using System.Net.Http;
using System.Threading.Tasks;
using SimpleNotesClient.Settings;

namespace SimpleNotesClient.WebApiQueries
{
    public class PutQueryBase : WebApiQueryBase
    {
        public object PutData { get; set; }

        public PutQueryBase(params string[] segments) : base(ApplicationSettings.UrlAPI, segments) { }

        public override async Task<HttpResponseMessage> ExecuteAsync() => await UrlFull.PutJsonAsync(PutData);
    }
}