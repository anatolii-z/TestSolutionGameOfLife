using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TestSolutionGameOfLife
{
    internal class ColorConverter : IValueConverter
    {
        public static SolidColorBrush Dead => Brushes.LightGray;
        public static SolidColorBrush Alive => Brushes.Black;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is CellStatus)
            {
                return (CellStatus)value == CellStatus.Alive ? Alive : Dead;
            }
            return Dead;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is SolidColorBrush && (SolidColorBrush)value == Alive)
            {
                return CellStatus.Alive;
            }
            return CellStatus.Dead;
        }
    }
}
