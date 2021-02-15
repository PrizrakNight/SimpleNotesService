using System.Text.Json.Serialization;

namespace SimpleNotesServer.Sdk.Models.Note
{
    public class DetailedNoteModel : NoteModel
    {
        [JsonPropertyName("created")]
        public long Created { get; set; }

        [JsonPropertyName("changed")]
        public int Changed { get; set; }
    }
}
