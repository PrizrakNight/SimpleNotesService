using System.Globalization;
using System.Windows.Controls;
using SimpleNotesClient.Managers;

namespace SimpleNotesClient.ValidationRules
{
    public class PasswordValidation : ValidationRule
    {
        private readonly string _invalidMessage = "Пароль должен быть от 8 до 15 символов и содержать хотя-бы один спец символ, цифру и заглавную букву";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (AuthManager.PasswordIsValid((string) value)) return ValidationResult.ValidResult;

            return new ValidationResult(false, _invalidMessage);
        }
    }
}