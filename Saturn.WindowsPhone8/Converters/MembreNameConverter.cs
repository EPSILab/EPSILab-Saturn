using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EPSILab.SolarSystem.Saturn.WindowsPhone8.Converters
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

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}