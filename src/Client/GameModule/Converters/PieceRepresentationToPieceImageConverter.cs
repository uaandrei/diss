using System.Windows.Data;

namespace Chess.Game.Converters
{
    public class PieceRepresentationToPieceImageConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string piece = value as string;
            if (piece != null && !string.IsNullOrEmpty(piece))
            {
                return string.Format("/Chess.Game;component/PieceImages/{0}.png", value);
            }
            return null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
