using System.Net;

namespace SimpleNotesClient.Models.Queries
{
    public struct WebQueryFailed
    {
        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}