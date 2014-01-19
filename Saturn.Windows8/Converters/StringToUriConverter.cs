using System;
using Windows.UI.Xaml.Data;

namespace EPSILab.SolarSystem.Saturn.Windows8.Converters
{
    /// <summary>
    /// A converter which transforms a string to an URI
    /// </summary>
    public sealed class StringToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string && Uri.IsWellFormedUriString(value.ToString(), UriKind.Absolute))
            {
                Uri uri = new Uri(value.ToString(), UriKind.Absolute);
                return uri;
            }

            return value;
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