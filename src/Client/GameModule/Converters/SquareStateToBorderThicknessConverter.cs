﻿using Chess.Infrastructure.Enums;
using System;
using System.Windows.Data;

namespace Chess.Game.Converters
{
    public class SquareStateToBorderThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is SquareState)
            {
                var state = (SquareState)value;
                return state == SquareState.Empty ? 0 : 10;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
