using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SimpleNotes.Server.Application.Models.Responses
{
    public class NoteResponse
    {
        [JsonPropertyName("key")]
        [DefaultValue(1)]
        public int Key { get; set; }

        [JsonPropertyName("name")]
        [DefaultValue("New note")]
        public string Name { get; set; }

        [JsonPropertyName("content")]
        [DefaultValue("The note content")]
        public string Content { get; set; }

        [JsonPropertyName("created")]
        [DefaultValue(592133123)]
        public long Created { get; set; }

        [JsonPropertyName("changed")]
        [DefaultValue(592133123)]
        public long Changed { get; set; }
    }
}
