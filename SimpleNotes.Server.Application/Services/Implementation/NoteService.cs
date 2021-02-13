using Mapster;
using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Models.Responses;
using SimpleNotes.Server.Domain.Contracts;
using SimpleNotes.Server.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleNotes.Server.Application.Services.Implementation
{
    internal class NoteService : INoteService
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public NoteService(IUserAccessor userAccessor, IRepositoryWrapper repositoryWrapper)
        {
            _userAccessor = userAccessor;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<NoteResponse> CreateNoteAsync(NoteRequest noteRequest)
        {
            var simpleNote = noteRequest.Adapt<SimpleNote>();

            simpleNote.Created = simpleNote.Changed = DateTimeOffset.Now.ToUnixTimeSeconds();
            simpleNote.UserId = _userAccessor.CurrentUserId;

            var createdNote = await _repositoryWrapper.Notes.InsertAsync(simpleNote);

            return createdNote.Adapt<NoteResponse>();
        }

        public async Task<bool> DeleteNoteAsync(int noteId)
        {
            var userNote = _userAccessor.CurrentUser.Notes.FirstOrDefault(note => note.KeyEqualTo(noteId));

            if(userNote != default)
            {
                await _repositoryWrapper.Notes.DeleteAsync(userNote);

                return true;
            }

            return false;
        }

        public Task<NoteResponse[]> GetNotesAsync()
        {
            var response = _userAccessor.CurrentUser.Notes.Select(note => note.Adapt<NoteResponse>()).ToArray();

            return Task.FromResult(response);
        }

        public async Task<NoteResponse> UpdateNoteAsync(NoteRequest noteRequest)
        {
            var simpleNote = noteRequest.Adapt<SimpleNote>();
            var changedNote = await _repositoryWrapper.Notes.UpdateAsync(simpleNote);

            return changedNote.Adapt<NoteResponse>();
        }
    }
}
