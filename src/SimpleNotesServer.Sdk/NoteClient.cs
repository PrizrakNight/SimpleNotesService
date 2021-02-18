using SimpleNotesServer.Sdk.Contracts;
using SimpleNotesServer.Sdk.Models.Note;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleNotesServer.Sdk
{
    public class NoteClient : INoteClient
    {
        private readonly HttpClient _httpClient;

        public NoteClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DetailedNoteModel> CreateNoteAsync(NoteModel noteModel)
        {
            var response = await _httpClient.SendObjectAsync<DetailedNoteModel>("notes", "POST", noteModel);

            return response;
        }

        public async Task DeleteNoteAsync(int noteKey)
        {
            await _httpClient.DeleteAsync($"notes/{noteKey}");
        }

        public async Task<DetailedNoteModel[]> GetNotesAsync()
        {
            var response = await _httpClient.GetObjectAsync<DetailedNoteModel[]>("notes");

            return response;
        }

        public async Task<DetailedNoteModel> UpdateNoteAsync(NoteModel noteModel)
        {
            var response = await _httpClient.SendObjectAsync<DetailedNoteModel>("notes", "PATCH", noteModel);

            return response;
        }
    }
}
