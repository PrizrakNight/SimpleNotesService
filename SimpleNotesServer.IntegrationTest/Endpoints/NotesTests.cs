using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Models.Responses;
using SimpleNotesServer.IntegrationTest.Clients;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace SimpleNotesServer.IntegrationTest.Endpoints
{
    public class NotesTests : IClassFixture<ApplicationAuthorizedClient>
    {
        private readonly ApplicationAuthorizedClient _applicationAuthorizedClient;

        public NotesTests(ApplicationAuthorizedClient applicationAuthorizedClient)
        {
            _applicationAuthorizedClient = applicationAuthorizedClient;
        }

        [Fact]
        public async Task Post_ShouldCreateNote()
        {
            var request = new NoteRequest
            {
                Name = "Example of note",
                Content = "Example of content"
            };

            var note = await CreateNoteAsync(request);

            Assert.Equal(request.Name, note.Name);
            Assert.Equal(request.Content, note.Content);
            Assert.True(note.Key != 0);
        }

        [Fact]
        public async Task Patch_ShouldUpdateTestNote()
        {
            var note = await CreateNoteAsync(new NoteRequest
            {
                Name = "Note for update",
                Content = "Content for update"
            });

            var request = new NoteRequest
            {
                Name = "Example of note",
                Content = "Example of content",
                Key = note.Key
            };

            var jsonString = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var message = new HttpRequestMessage
            {
                Content = content,
                RequestUri = new Uri("http://localhost/api/notes"),
                Method = new HttpMethod("PATCH")
            };

            var responseMessage = await _applicationAuthorizedClient.Client.SendAsync(message);
            var responseJson = await responseMessage.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);

            var responseNote = JsonSerializer.Deserialize<NoteResponse>(responseJson);

            Assert.Equal(request.Name, responseNote.Name);
            Assert.Equal(request.Content, responseNote.Content);
            Assert.True(note.Key != 0);
        }

        [Fact]
        public async Task Delete_ShouldDeleteTestNote()
        {
            var note = await CreateNoteAsync(new NoteRequest
            {
                Name = "Note for delete",
                Content = "Content for delete"
            });

            var response = await _applicationAuthorizedClient.Client.DeleteAsync($"api/notes/{note.Key}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task<NoteResponse> CreateNoteAsync(NoteRequest request)
        {
            var jsonString = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _applicationAuthorizedClient.Client.PostAsync("/api/notes", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            return JsonSerializer.Deserialize<NoteResponse>(responseJson);
        }
    }
}
