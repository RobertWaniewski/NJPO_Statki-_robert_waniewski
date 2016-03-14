using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using BitwaMorskoLadowa.Model;
using System.Windows.Media;
using System.Globalization;

namespace BitwaMorskoLadowa.View
{
    [ValueConversion(typeof(SquareType), typeof(Brush))]
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            SquareType type = (SquareType)value;

            switch (type)
            {
                case SquareType.Poczatkowy:
                    return new SolidColorBrush(Colors.LightSlateGray);
                case SquareType.Woda:
                    return new SolidColorBrush(Colors.RoyalBlue);
                case SquareType.WodTraf:
                    return new SolidColorBrush(Colors.Blue);
                case SquareType.Trawa:
                    return new SolidColorBrush(Colors.YellowGreen);
                case SquareType.TrawaTraf:
                    return new SolidColorBrush(Colors.ForestGreen);
                case SquareType.NieTrafiony:
                    return new SolidColorBrush(Colors.Black);
                case SquareType.Trafiony:
                    return new SolidColorBrush(Colors.Yellow);
                case SquareType.Zniszczony:
                    return new SolidColorBrush(Colors.DarkRed);
            }

            throw new Exception("fail");
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
