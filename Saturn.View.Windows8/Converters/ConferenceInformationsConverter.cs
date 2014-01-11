using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.Win8.Resources;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace SolarSystem.Saturn.Win8.Converters
{
    /// <summary>
    /// A converter used by the Conference details page.
    /// Display informations below the passed parameter
    /// </summary>
    public sealed class ConferenceInformationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Conference && parameter is string)
            {
                Conference conference = value as Conference;

                IDictionary<string, string> informations = new Dictionary<string, string>
                {
                    { "StartDate", string.Format(FormatsRsxAccessor.GetString("Conference_StartDate"), conference.Date_Heure_Debut) },
                    { "EndDate", string.Format(FormatsRsxAccessor.GetString("Conference_EndDate"), conference.Date_Heure_Fin) },
                    { "Location", string.Format(FormatsRsxAccessor.GetString("Conference_Location"), conference.Lieu, conference.Ville.Libelle) }
                };

                return informations[parameter.ToString()];
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}