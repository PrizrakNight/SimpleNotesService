namespace SimpleNotesServer.Data.Models.Notes
{
    public interface IServerNote
    {
        long Created { get; set; }

        long Changed { get; set; }

        string Content { get; set; }
    }
}