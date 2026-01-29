using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Lab2
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Drawing.Color drawingColor)
            {
                return new SolidColorBrush(System.Windows.Media.Color.FromArgb(
                    drawingColor.A,
                    drawingColor.R,
                    drawingColor.G,
                    drawingColor.B
                ));
            }
            return new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Windows.Media.Color wpfColor)
            {
                return System.Drawing.Color.FromArgb(
                    wpfColor.A,
                    wpfColor.R,
                    wpfColor.G,
                    wpfColor.B
                );
            }
            if (value is SolidColorBrush brush && brush.Color is System.Windows.Media.Color brushColor)
            {
                return System.Drawing.Color.FromArgb(
                    brushColor.A,
                    brushColor.R,
                    brushColor.G,
                    brushColor.B
                );
            }
            return System.Drawing.Color.LightGray;
        }
    }
}
