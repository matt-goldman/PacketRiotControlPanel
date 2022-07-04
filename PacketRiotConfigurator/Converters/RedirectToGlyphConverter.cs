using System.Globalization;

namespace PacketRiotConfigurator.Converters;

public class RedirectToGlyphConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var redirect = (bool)value;

        if (redirect)
        {
            return IconFont.Sync;
        }
        else
        {
            return IconFont.ArrowRight;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
