﻿using Mapster;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<NoteService> _logger;

        public NoteService(IUserAccessor userAccessor, IRepositoryWrapper repositoryWrapper, ILogger<NoteService> logger)
        {
            _userAccessor = userAccessor;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<NoteResponse> CreateNoteAsync(NoteRequest noteRequest)
        {
            _logger.LogInformation("Creating a note {@noteRequest}...", noteRequest);

            var simpleNote = noteRequest.Adapt<SimpleNote>();

            simpleNote.Key = 0;
            simpleNote.Created = simpleNote.Changed = DateTimeOffset.Now.ToUnixTimeSeconds();
            simpleNote.UserId = _userAccessor.CurrentUserId;

            var createdNote = await _repositoryWrapper.Notes.InsertAsync(simpleNote);

            await _repositoryWrapper.SaveAsync();

            return createdNote.Adapt<NoteResponse>();
        }

        public async Task<bool> DeleteNoteAsync(int noteId)
        {
            _logger.LogInformation("Deleting a note with id '{noteId}'...", noteId);

            var userNote = _userAccessor.CurrentUser.Notes.FirstOrDefault(note => note.Key == noteId);

            if (userNote != default)
            {
                await _repositoryWrapper.Notes.DeleteAsync(userNote);
                await _repositoryWrapper.SaveAsync();

                return true;
            }

            return false;
        }

        public Task<NoteResponse[]> GetNotesAsync()
        {
            _logger.LogInformation("Retrieving all notes ...");

            var response = _userAccessor.CurrentUser.Notes.Select(note => note.Adapt<NoteResponse>()).ToArray();

            return Task.FromResult(response);
        }

        public async Task<NoteResponse> UpdateNoteAsync(NoteRequest noteRequest)
        {
            _logger.LogInformation("Updating a note with id '{noteId}' to '{@noteRequest}'", noteRequest.Key, noteRequest);

            var simpleNote = noteRequest.Adapt<SimpleNote>();
            var changedNote = await _repositoryWrapper.Notes.UpdateAsync(simpleNote);

            await _repositoryWrapper.SaveAsync();

            return changedNote.Adapt<NoteResponse>();
        }
    }
}
