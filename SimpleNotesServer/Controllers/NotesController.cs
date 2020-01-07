using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleNotesServer.Data.Contexts;
using SimpleNotesServer.Data.Models.Notes;
using SimpleNotesServer.Data.Models.Responses;
using SimpleNotesServer.Data.Models.Users;
using SimpleNotesServer.Data.Repositories;
using SimpleNotesServer.Extensions.Repository;

namespace SimpleNotesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : RepositoryController<SimpleServerRepository, BaseServerContext>
    {
        [Authorize]
        [HttpGet("all")]
        public ObjectResult GetNotes()
        {
            IEnumerable<SimpleNote> notes = repository.GetUserByClaimsPrincipal(User).Notes;

            if (notes == default || notes.Count() == 0)
                return Ok($"На данный момент у вас нету заметок");

            return Ok(notes);
        }

        [Authorize]
        [HttpGet("myinfo")]
        public UserInfo GetUserInfo() => new UserInfo(repository.GetUserByClaimsPrincipal(User));

        [Authorize]
        [HttpPost("add")]
        public HttpResponseMessage AddNewNote([FromBody] SimpleNote newNote)
        {
            SimpleUser needleUser = repository.GetUserByClaimsPrincipal(User);

            if (ModelState.IsValid)
            {
                needleUser.Notes.Add(new SimpleNote(newNote));

                repository.SaveChangesAsync();
            }

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Authorize]
        [HttpPut("change/{key}")]
        public HttpResponseMessage ChangeNote([FromBody] SimpleNote changedNote, int key)
        {
            if (ModelState.IsValid)
            {
                SimpleUser needleUser = repository.GetUserByClaimsPrincipal(User);
                SimpleNote needleNote = needleUser.GetNoteByKey(key);

                if (needleNote != default)
                {
                    needleNote.ChangeMe(changedNote);
                    repository.SaveChangesAsync();

                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [Authorize]
        [HttpDelete("delete/{key}")]
        public HttpResponseMessage DeleteNote(int key)
        {
            SimpleUser needleUser = repository.GetUserByClaimsPrincipal(User);
            SimpleNote needleNote = needleUser.GetNoteByKey(key);

            if (needleNote != default)
            {
                needleUser.Notes.Remove(needleNote);
                repository.SaveChangesAsync();

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
