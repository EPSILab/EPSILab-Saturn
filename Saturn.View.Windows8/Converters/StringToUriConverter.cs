using System;
using Windows.UI.Xaml.Data;

namespace SolarSystem.Saturn.Win8.Converters
{
    public sealed class StringToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string)
            {
                Uri uri = new Uri(value.ToString(), UriKind.Absolute);
                return uri;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Uri)
            {
                Uri uri = value as Uri;
                return uri.AbsoluteUri;
            }

            return null;
        }
    }
}
