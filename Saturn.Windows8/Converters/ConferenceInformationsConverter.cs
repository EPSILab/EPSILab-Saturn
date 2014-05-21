using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.Windows8.Resources;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace EPSILab.SolarSystem.Saturn.Windows8.Converters
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
                    { "Start_DateTime", string.Format(FormatsRsxAccessor.GetString("Conference_Start_DateTime"), conference.Start_DateTime) },
                    { "EndDate", string.Format(FormatsRsxAccessor.GetString("Conference_EndDate"), conference.End_DateTime) },
                    { "Location", string.Format(FormatsRsxAccessor.GetString("Conference_Location"), conference.Place, conference.Campus.Place) }
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