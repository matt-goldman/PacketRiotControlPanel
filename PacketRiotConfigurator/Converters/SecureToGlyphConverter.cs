using System.Globalization;

namespace PacketRiotConfigurator.Converters
{
    public class SecureToGlyphConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var secure = (bool)value;

            if (secure)
            {
                return IconFont.Lock;
            }
            else
            {
                return IconFont.Unlock;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
