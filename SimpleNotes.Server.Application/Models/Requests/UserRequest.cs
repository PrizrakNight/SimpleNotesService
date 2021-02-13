using System.ComponentModel.DataAnnotations;

namespace SimpleNotes.Server.Application.Models.Requests
{
    public class UserRequest
    {
        [Required(ErrorMessage = "Имя пользователя обязательно к заполнению")]
        [RegularExpression(@"^[a-zA-Z0-9_-]{3,20}$")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Пароль обязателен к заполнению")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
