using SimpleNotesServer.Sdk.Models.Note;
using System.Threading.Tasks;

namespace SimpleNotesServer.Sdk.Contracts
{
    public interface INoteClient
    {
        Task<DetailedNoteModel[]> GetNotesAsync();

        Task<DetailedNoteModel> UpdateNoteAsync(NoteModel noteModel);
        Task<DetailedNoteModel> CreateNoteAsync(NoteModel noteModel);

        Task DeleteNoteAsync(int noteKey);
    }
}
