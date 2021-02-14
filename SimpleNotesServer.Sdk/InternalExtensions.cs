using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleNotesServer.Sdk
{
    internal static class InternalExtensions
    {
        public static async Task<TResponse> SendObjectAsync<TResponse>(this HttpClient httpClient, string url, string method, object @object)
        {
            var requestMessage = new HttpRequestMessage
            {
                Content = GetStringContent(@object),
                Method = new HttpMethod(method)
            };

            var responseMessage = await httpClient.SendAsync(requestMessage);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponse>(jsonString);
        }

        public static async Task<TResponse> GetObjectAsync<TResponse>(this HttpClient httpClient, string url)
        {
            var responseMessage = await httpClient.GetAsync(url);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponse>(jsonString);
        }

        private static StringContent GetStringContent(object @object)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(@object), Encoding.UTF8, "application/json");

            return httpContent;
        }
    }
}
