using System.Globalization;

namespace PacketRiotConfigurator.Converters;

public class BoolToBackgroundConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var isRunning = (bool)value;

        if (isRunning)
        {
            return Colors.OrangeRed;
        }
        else
        {
            return Colors.ForestGreen;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
