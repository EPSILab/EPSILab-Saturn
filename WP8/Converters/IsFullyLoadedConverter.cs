using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SolarSystem.Saturn.WP8.Converters
{
    public class IsFullyLoadedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (App.IsInternetAvailable && value is bool && (bool) value)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return App.IsInternetAvailable && value is Visibility && (Visibility) value == Visibility.Collapsed;
        }
    }
}