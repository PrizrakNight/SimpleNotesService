using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleNotes.Server.Application.Filters;
using SimpleNotes.Server.Application.Models.Requests;
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
        public async Task<IActionResult> GetNotesAsync()
        {
            return Ok(await _noteService.GetNotesAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateNoteAsync([FromBody] NoteRequest noteRequest)
        {
            return Ok(await _noteService.CreateNoteAsync(noteRequest));
        }

        [HttpPatch]
        [ServiceFilter(typeof(UserHasNoteFilterAttribute))]
        public async Task<IActionResult> UpdateNoteAsync([FromBody] NoteRequest noteRequest)
        {
            return Ok(await _noteService.UpdateNoteAsync(noteRequest));
        }

        [HttpDelete("{noteKey}")]
        [ServiceFilter(typeof(UserHasNoteFilterAttribute))]
        public async Task<IActionResult> DeleteNoteAsync(int noteKey)
        {
            await _noteService.DeleteNoteAsync(noteKey);

            return Ok();
        }
    }
}
