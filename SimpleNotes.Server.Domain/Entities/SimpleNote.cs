using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleNotes.Server.Domain.Entities
{
    public class SimpleNote : EntityBase
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Заметка должна иметь имя")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Название заметки должно быть от 3 до 30 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Содержимое заметки не должно быть пустым")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Дата создания должна быть указана")]
        public long Created { get; set; }

        [Required(ErrorMessage = "Дата изменения должна быть указана")]
        public long Changed { get; set; }

        public SimpleUser User { get; set; }
    }
}