using System;
using System.Collections;
using Windows.UI.Xaml.Data;

namespace SolarSystem.Saturn.Win8.Converters
{
    /// <summary>
    /// A converter which determines if an object is null
    /// </summary>
    public sealed class NullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                return true;
            }

            if (value is IList && (value as IList).Count > 0)
            {
                return true;
            }

            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}