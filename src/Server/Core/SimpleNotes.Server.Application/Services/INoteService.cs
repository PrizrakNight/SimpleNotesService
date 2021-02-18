using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Models.Responses;
using System.Threading.Tasks;

namespace SimpleNotes.Server.Application.Services
{
    public interface INoteService
    {
        Task<bool> DeleteNoteAsync(int noteId);

        Task<NoteResponse> CreateNoteAsync(NoteRequest noteRequest);
        Task<NoteResponse> UpdateNoteAsync(NoteRequest noteRequest);

        Task<NoteResponse[]> GetNotesAsync();
    }
}
