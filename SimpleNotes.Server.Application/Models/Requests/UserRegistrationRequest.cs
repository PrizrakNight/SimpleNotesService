using System.ComponentModel.DataAnnotations;

namespace SimpleNotes.Server.Application.Models.Requests
{
    public class UserRegistrationRequest : UserRequest
    {
        [Required(ErrorMessage = "Подтверждение пароля обязательно")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [RegularExpression(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$")]
        public string AvatarUrl { get; set; }
    }
}
