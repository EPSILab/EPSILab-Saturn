using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.Win8.Resources;
using System;
using Windows.UI.Xaml.Data;

namespace SolarSystem.Saturn.Win8.Converters
{
    public sealed class ConferenceInformationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Conference && parameter is string)
            {
                Conference conference = value as Conference;

                if (parameter.ToString() == "StartDate")
                {
                    return string.Format(FormatsRsxAccessor.GetString("CONFERENCE_STARTDATE_FORMAT"), conference.Date_Heure_Debut);
                }
                
                if (parameter.ToString() == "EndDate")
                {
                    return string.Format(FormatsRsxAccessor.GetString("CONFERENCE_ENDDATE_FORMAT"), conference.Date_Heure_Fin);
                }

                if (parameter.ToString() == "Location")
                {
                    return string.Format(FormatsRsxAccessor.GetString("CONFERENCE_LOCATION_FORMAT"), conference.Lieu, conference.Ville.Libelle);
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}