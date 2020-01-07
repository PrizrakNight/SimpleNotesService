using System.ComponentModel.DataAnnotations;

namespace SimpleNotesServer.Data.Models.Requests
{
    public struct RequestUser
    {
        [Required(ErrorMessage = "Имя пользователя обязательно к заполнению")]
        [RegularExpression(@"^[a-zA-Z0-9_-]{3,20}$")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Пароль обязателен к заполнению")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$")]
        public string Password { get; set; }
    }
}