namespace SimpleNotesServer.Sdk.Models.Note
{
    public class DetailedNoteModel : NoteModel
    {
        public long Created { get; set; }

        public int Changed { get; set; }
    }
}
