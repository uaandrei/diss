using Chess.Infrastructure.Enums;
using System.Windows.Data;

namespace Chess.Game.Converters
{
    public class SquareStateToBorderColorConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is SquareState)
            {
                var state = (SquareState)value;
                switch (state)
                {
                    case SquareState.Empty:
                        break;
                    case SquareState.PosibleAttack:
                        return System.Windows.Media.Brushes.Red;
                    case SquareState.PosibleMove:
                        return System.Windows.Media.Brushes.Yellow;
                    case SquareState.Selected:
                        return System.Windows.Media.Brushes.Green;
                    case SquareState.LastMove:
                        return System.Windows.Media.Brushes.Aqua;
                    default:
                        break;
                }
            }
            return null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
