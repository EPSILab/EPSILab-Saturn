using SolarSystem.Saturn.ViewModel;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SolarSystem.Saturn.View.WindowsPhone.Converters
{
    public class NetworkAvailableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!App.IsInternetAvailable && !ViewModelLocator.MainVM.IsLoading)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}