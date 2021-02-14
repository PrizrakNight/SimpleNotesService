using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleNotes.Server.Application.Models.Requests
{
    public class UserRegistrationRequest : UserRequest
    {
        [DefaultValue("My_SecretPassw0rd")]
        [Required(ErrorMessage = "Password confirmation is required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [DefaultValue("https://www.example.com/images/avatar.png")]
        [RegularExpression(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$")]
        public string AvatarUrl { get; set; }
    }
}
