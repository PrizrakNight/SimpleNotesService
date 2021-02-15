using System.Text.Json.Serialization;

namespace SimpleNotesServer.Sdk.Models.Note
{
    public class NoteModel
    {
        [JsonPropertyName("key")]
        public int Key { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
