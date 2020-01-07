using System.Net.Http;
using System.Threading.Tasks;
using SimpleNotesClient.WebApiQueries.Abstract;

namespace SimpleNotesClient.WebApiQueries
{
    public abstract class WebApiQueryBase : IWebApiQuery
    {
        public string UrlBase { get; private set; }

        public string UrlSegments { get; private set; }

        public string UrlFull => $"{UrlBase}/{UrlSegments}";

        public WebApiQueryBase(string urlBase)
        {
            UrlBase = urlBase;
            UrlSegments = string.Empty;
        }

        public WebApiQueryBase(string urlBase, params string[] segments) : this(urlBase) => SetSegments(segments);

        public abstract Task<HttpResponseMessage> ExecuteAsync();

        public void SetSegments(params string[] segments) => UrlSegments = $"{string.Join("/", segments)}/";

        public void AppendSegment(params string[] segments) => UrlSegments += $"{string.Join("/", segments)}/";

        public void ResetSegments() => UrlSegments = string.Empty;
    }
}