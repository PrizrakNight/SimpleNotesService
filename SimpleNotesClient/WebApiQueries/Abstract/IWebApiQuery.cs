using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleNotesClient.WebApiQueries.Abstract
{
    public interface IWebApiQuery
    {
        #region Url

        string UrlBase { get; }

        string UrlSegments { get; }

        string UrlFull { get; }

        #endregion

        Task<HttpResponseMessage> ExecuteAsync();

        #region UrlSegments

        void SetSegments(params string[] segments);

        void AppendSegment(params string[] segments);

        void ResetSegments();

        #endregion
    }
}