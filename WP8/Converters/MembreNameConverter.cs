using SolarSystem.Saturn.DataAccess.Webservice;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SolarSystem.Saturn.WP8.Converters
{
    public class MembreNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Membre)
            {
                Membre membre = value as Membre;
                return string.Format(CultureInfo.CurrentUICulture, "{0} {1}", membre.Prenom, membre.Nom);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

