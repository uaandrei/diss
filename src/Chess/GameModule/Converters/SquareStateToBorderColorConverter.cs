using Chess.Infrastructure.Enums;
using System.Windows.Data;

namespace Chess.Game.Converters
{
    public class SquareStateToBorderColorConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is SquareStates)
            {
                var state = (SquareStates)value;
                switch (state)
                {
                    case SquareStates.Empty:
                        break;
                    case SquareStates.PosibleAttack:
                        return System.Windows.Media.Brushes.Red;
                    case SquareStates.PosibleMove:
                        return System.Windows.Media.Brushes.Yellow;
                    case SquareStates.Selected:
                        return System.Windows.Media.Brushes.Green;
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
