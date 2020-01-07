using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SimpleNotesClient.Models;
using SimpleNotesClient.WebApiQueries.Authorized;

namespace SimpleNotesClient.Managers
{
    public static class DataManager
    {
        public static async Task<IEnumerable<SimpleNote>> GetAllNotesAsync()
        {
            HttpResponseMessage response = await new AuthorizedGetQuery("notes", "all").ExecuteAsync();

            return JArray.Parse(await response.Content.ReadAsStringAsync()).Select(token => new SimpleNote
            {
                Name = (string) token["name"],
                Content = (string) token["content"],
                Key = Convert.ToInt32(token["key"]),
                DateCreated = DateTimeOffset.FromUnixTimeSeconds((long) token["created"]).DateTime.AddHours(3),
                DateChanged = DateTimeOffset.FromUnixTimeSeconds((long) token["changed"]).DateTime.AddHours(3)
            });
        }

        public static async Task<HttpResponseMessage> CreateNote(SimpleNote newNote)
        {
            HttpResponseMessage response = await new AuthorizedPostQuery("notes", "add")
            {
                PostData = newNote
            }.ExecuteAsync();

            return response;
        }

        public static async Task<string> GetAvatar()
        {
            HttpResponseMessage response = await new AuthorizedGetQuery("account", "options").ExecuteAsync();

            return (string) JObject.Parse(await response.Content.ReadAsStringAsync())["avatarURL"];
        }
    }
}