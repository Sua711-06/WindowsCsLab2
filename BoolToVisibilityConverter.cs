using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Lab2
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isTrue)
            {
                bool invert = parameter != null && parameter.ToString().Equals("Inverted", StringComparison.OrdinalIgnoreCase);
                if (invert)
                {
                    return isTrue ? Visibility.Collapsed : Visibility.Visible;
                }
                return isTrue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                bool invert = parameter != null && parameter.ToString().Equals("Inverted", StringComparison.OrdinalIgnoreCase);
                if (invert)
                {
                    return visibility == Visibility.Collapsed;
                }
                return visibility == Visibility.Visible;
            }
            return false;
        }
    }
}
