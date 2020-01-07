using System;
using System.Globalization;
using System.Windows.Data;

namespace SimpleNotesClient.Converters
{
    public class DateFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = (DateTime) value;

            string prefix = (string) parameter;

            return $"{prefix} " + dateTime.ToString("dd.MM.yyyy h:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string dateTarget = (string) value;

            return DateTime.Parse(dateTarget.Split(' ')[1]);
        }
    }
}