using System.ComponentModel.DataAnnotations;

namespace SimpleNotes.Server.Application.Models.Requests
{
    public class NoteRequest
    {
        public int Key { get; set; }

        [Required(ErrorMessage = "Заметка должна иметь имя")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Название заметки должно быть от 3 до 30 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Содержимое заметки не должно быть пустым")]
        public string Content { get; set; }
    }
}
