using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleNotes.Server.Application.Filters;
using SimpleNotes.Server.Application.Models.Requests;
using SimpleNotes.Server.Application.Models.Responses;
using SimpleNotes.Server.Application.Services;
using System.Threading.Tasks;

namespace SimpleNotesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(BadResponse), 401)]
        [ProducesResponseType(typeof(NoteResponse[]), 200)]
        public async Task<IActionResult> GetNotesAsync()
        {
            return Ok(await _noteService.GetNotesAsync());
        }

        [HttpPost]
        [ProducesResponseType(typeof(BadResponse), 401)]
        [ProducesResponseType(typeof(NoteResponse), 200)]
        public async Task<IActionResult> CreateNoteAsync([FromBody] NoteRequest noteRequest)
        {
            return Ok(await _noteService.CreateNoteAsync(noteRequest));
        }

        [HttpPatch]
        [ProducesResponseType(typeof(BadResponse), 401)]
        [ProducesResponseType(typeof(BadResponse), 400)]
        [ProducesResponseType(typeof(NoteResponse), 200)]
        [ServiceFilter(typeof(UserHasNoteFilterAttribute))]
        public async Task<IActionResult> UpdateNoteAsync([FromBody] NoteRequest noteRequest)
        {
            return Ok(await _noteService.UpdateNoteAsync(noteRequest));
        }

        [HttpDelete("{noteKey}")]
        [ProducesResponseType(typeof(BadResponse), 401)]
        [ProducesResponseType(typeof(BadResponse), 400)]
        [ServiceFilter(typeof(UserHasNoteFilterAttribute))]
        public async Task<IActionResult> DeleteNoteAsync(int noteKey)
        {
            await _noteService.DeleteNoteAsync(noteKey);

            return Ok();
        }
    }
}
