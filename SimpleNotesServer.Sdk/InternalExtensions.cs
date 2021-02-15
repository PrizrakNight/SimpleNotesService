using SimpleNotesServer.Sdk.Exceptions;
using System;
using System.Net;
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
            var responseMessage = new HttpResponseMessage();
            var jsonString = string.Empty;

            try
            {
                var requestMessage = new HttpRequestMessage
                {
                    Content = GetStringContent(@object),
                    Method = new HttpMethod(method),
                    RequestUri = new Uri(httpClient.BaseAddress + url)
                };

                responseMessage = await httpClient.SendAsync(requestMessage);
                jsonString = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.StatusCode != HttpStatusCode.OK)
                    throw new InvalidRequestException(jsonString, responseMessage.StatusCode, default);

                return JsonSerializer.Deserialize<TResponse>(jsonString);
            }
            catch (Exception exception)
            {
                throw new InvalidRequestException(jsonString, responseMessage.StatusCode, exception);
            }
        }

        public static async Task<TResponse> GetObjectAsync<TResponse>(this HttpClient httpClient, string url)
        {
            var responseMessage = new HttpResponseMessage();
            var jsonString = string.Empty;

            try
            {
                responseMessage = await httpClient.GetAsync(url);
                jsonString = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.StatusCode != HttpStatusCode.OK)
                    throw new InvalidRequestException(jsonString, responseMessage.StatusCode, default);

                return JsonSerializer.Deserialize<TResponse>(jsonString);
            }
            catch (Exception exception)
            {
                throw new InvalidRequestException(jsonString, responseMessage.StatusCode, exception);
            }
        }

        private static StringContent GetStringContent(object @object)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(@object), Encoding.UTF8, "application/json");

            return httpContent;
        }
    }
}
