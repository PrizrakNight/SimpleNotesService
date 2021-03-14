using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Models.Responses;
using System.Collections.Generic;
using System.Linq;

namespace SimpleNotes.Server.Application.Filters
{
    public class UserHasNoteFilterAttribute : ActionFilterAttribute
    {
        private readonly IUserAccessor _userAccessor;
        private readonly ILogger<UserHasNoteFilterAttribute> _logger;

        public UserHasNoteFilterAttribute(IUserAccessor userAccessor, ILogger<UserHasNoteFilterAttribute> logger)
        {
            _userAccessor = userAccessor;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var noteKey = ExtractNoteKey(context.ActionArguments);

            _logger.LogInformation("Trying to check if a user has a note with identifier '{noteId}'...", noteKey);

            if (noteKey != -1)
            {
                if(!_userAccessor.CurrentUser.Notes.Any(note => note.Key == noteKey))
                {
                    context.Result = new BadRequestObjectResult(new BadResponse
                    {
                        StatusCode = 404,
                        Message = $"Note with key '{noteKey}' not found on user"
                    });

                    _logger.LogWarning("User does not have a note with id '{noteId}'", noteKey);
                }
            }
        }

        private int ExtractNoteKey(IDictionary<string, object> actionArguments)
        {
            foreach (var keyValue in actionArguments)
            {
                if (keyValue.Value is int noteKey)
                    return noteKey;

                if (keyValue.Value is NoteRequest noteRequest)
                    return noteRequest.Key;
            }

            return -1;
        }
    }
}
