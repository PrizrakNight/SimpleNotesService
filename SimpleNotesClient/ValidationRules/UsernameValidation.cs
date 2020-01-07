using System.Globalization;
using System.Windows.Controls;
using SimpleNotesClient.Managers;

namespace SimpleNotesClient.ValidationRules
{
    public class UsernameValidation : ValidationRule
    {
        private readonly string _invalidMessage = "Имя пользователя должно быть от 3 до 20 символов и не содержать пробелов.";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (AuthManager.UsernameIsValid((string) value)) return ValidationResult.ValidResult;

            return new ValidationResult(false, _invalidMessage);
        }
    }
}