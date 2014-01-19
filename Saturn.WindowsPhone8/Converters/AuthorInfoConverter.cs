using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.View.WindowsPhone.Resources;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SolarSystem.Saturn.View.WindowsPhone.Converters
{
    public class AuthorInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Membre)
            {
                Membre membre = value as Membre;
                return string.Format(CultureInfo.CurrentUICulture, AppResources.FORMAT_AUTHOR, membre.Prenom, membre.Nom);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
