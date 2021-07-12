using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CharacterManager.Views.Helpers
{
    public class WindowStateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.Parse( typeof( WindowState ), (string)value );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
