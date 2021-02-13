using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleNotes.Server.Domain.Entities
{
    public class SimpleUser : EntityBase
    {
        [Required(ErrorMessage = "Имя пользователя обязательно к заполнению")]
        [RegularExpression(@"^[a-zA-Z0-9_-]{3,20}$")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ссылка на аватар обязательна")]
        [RegularExpression(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$")]
        public string AvatarUrl { get; set; }

        [Required(ErrorMessage = "Пользователь обязан иметь пароль")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Дата регистрации дожна быть указана")]
        public long RegistrationDate { get; set; }

        [Required(ErrorMessage = "Дата последнего входа должна быть указана")]
        public long LastEntrance { get; set; }

        [Required(ErrorMessage = "Пользователь обязан иметь роль")]
        [StringLength(40, ErrorMessage = "Название роли не должно превышать 40 символов")]
        public string Role { get; set; }

        public ICollection<SimpleNote> Notes { get; set; }
    }
}