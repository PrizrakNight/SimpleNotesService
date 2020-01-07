using System.ComponentModel.DataAnnotations;

namespace SimpleNotesServer.Data.Models
{
    public class NamedEntityBase : EntityBase
    {
        [Required(ErrorMessage = "Сущность обязана иметь наименование")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Название сущности должно быть больше 2 символов и меньше 101 символа")]
        public string Name { get; set; }

        public bool CompareName(in string name) => Name == name;
    }
}