using System.ComponentModel;

namespace SimpleNotes.Server.Application.Models.Responses
{
    public class BadResponse
    {
        [DefaultValue(400)]
        public int StatusCode { get; set; }

        [DefaultValue("Some kind of error message")]
        public string Message { get; set; }
    }
}
