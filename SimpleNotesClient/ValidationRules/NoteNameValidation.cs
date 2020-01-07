using System.Globalization;
using System.Windows.Controls;

namespace SimpleNotesClient.ValidationRules
{
    public class NoteNameValidation : ValidationRule
    {
        private readonly string _invalidMessage = "Название должно быть не менее 3-х символов и не более 100 символов";

        public static bool CheckValidate(string target) =>
            !string.IsNullOrEmpty(target) && target.Length > 3 && target.Length <= 100;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = value?.ToString();

            if (CheckValidate(value?.ToString())) return ValidationResult.ValidResult;

            return new ValidationResult(false, _invalidMessage);
        }
    }
}