using System;
using System.Globalization;
using System.Windows.Data;

namespace SimpleNotesClient.Converters
{
    public class NotesCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int countNotes = System.Convert.ToInt32(value);

            if (countNotes >= 0) return $"{countNotes} заметок";
            else return "Нету заметок";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] data = value.ToString().Split(' ');

            if (data[0].ToLower() == "нету") return 0;
            else return int.Parse(data[0]);
        }
    }
}