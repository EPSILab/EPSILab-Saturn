using SolarSystem.Saturn.Model.ReadersService;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SolarSystem.Saturn.View.WindowsPhone.Converters
{
    public class MembreInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Membre)
            {
                Membre membre = value as Membre;
                return string.Format(CultureInfo.CurrentUICulture, "{0}, EPSI {1}", membre.Statut, membre.Ville.Libelle);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

