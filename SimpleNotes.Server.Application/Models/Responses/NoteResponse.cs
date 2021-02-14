using System.ComponentModel;

namespace SimpleNotes.Server.Application.Models.Responses
{
    public class NoteResponse
    {
        [DefaultValue(1)]
        public int Key { get; set; }

        [DefaultValue("New note")]
        public string Name { get; set; }

        [DefaultValue("The note content")]
        public string Content { get; set; }

        [DefaultValue(592133123)]
        public long Created { get; set; }

        [DefaultValue(592133123)]
        public long Changed { get; set; }
    }
}
