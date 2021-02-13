namespace SimpleNotes.Server.Application.Models.Responses
{
    public class NoteResponse
    {
        public int Key { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public long Created { get; set; }

        public long Changed { get; set; }
    }
}
