using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.View.WindowsPhone.Resources;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SolarSystem.Saturn.View.WindowsPhone.Converters
{
    public class ConferenceInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Conference)
            {
                Conference conference = value as Conference;
                return string.Format(CultureInfo.CurrentUICulture, AppResources.FORMAT_CONFERENCE, conference.Date_Heure_Debut, conference.Date_Heure_Fin, conference.Lieu);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
