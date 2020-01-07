using System.Globalization;
using System.Windows.Controls;

namespace SimpleNotesClient.ValidationRules
{
    public class NoteContentValidation : ValidationRule
    {
        public static bool CheckValidate(string target) => !string.IsNullOrEmpty(target) && target.Length > 0;

        private readonly string _invalidMessage = "Содержимое заметки не должно быть пустым.";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(CheckValidate(value?.ToString())) return ValidationResult.ValidResult;

            return new ValidationResult(false, _invalidMessage);
        }
    }
}