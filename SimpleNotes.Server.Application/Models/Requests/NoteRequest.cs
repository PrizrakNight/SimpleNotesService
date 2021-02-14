using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleNotes.Server.Application.Models.Requests
{
    public class NoteRequest
    {
        [DefaultValue(1)]
        public int Key { get; set; }

        [DefaultValue("New note")]
        [Required(ErrorMessage = "The note must have a name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Note title must be between 3 and 30 characters")]
        public string Name { get; set; }

        [DefaultValue("The note content")]
        [Required(ErrorMessage = "Note content must not be empty")]
        public string Content { get; set; }
    }
}
