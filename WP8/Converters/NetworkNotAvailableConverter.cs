using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using SolarSystem.Saturn.ViewModel;

namespace SolarSystem.Saturn.WP8.Converters
{
    public class NetworkNotAvailableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!App.IsInternetAvailable && !ViewModelLocator.MainVM.IsLoading)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}