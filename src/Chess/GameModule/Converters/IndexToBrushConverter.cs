using Chess.Infrastructure;
using System;
using System.Windows.Data;

namespace Chess.Game.Converters
{
    public class IndexToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                var index = (int)value;
                var x = index % 8;
                var y = index / 8;
                if (y % 2 == 0)
                {
                    return x % 2 == 0 ? System.Windows.Media.Brushes.Gray : System.Windows.Media.Brushes.White;
                }
                return x % 2 == 0 ? System.Windows.Media.Brushes.White : System.Windows.Media.Brushes.Gray;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
