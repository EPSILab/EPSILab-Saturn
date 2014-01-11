using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.Win8.Resources;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace SolarSystem.Saturn.Win8.Converters
{
    /// <summary>
    /// Converter used for the Show details page.
    /// Display show informations in terms of the parameter
    /// </summary>
    public sealed class SalonInformationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Salon && parameter is string)
            {
                Salon salon = value as Salon;

                IDictionary<string, string> informations = new Dictionary<string, string>
                {
                    { "StartDate", string.Format(FormatsRsxAccessor.GetString("Show_StartDate"), salon.Date_Heure_Debut)},
                    { "EndDate", string.Format(FormatsRsxAccessor.GetString("Show_EndDate"), salon.Date_Heure_Fin)},
                    { "Location", string.Format(FormatsRsxAccessor.GetString("Show_Location"), salon.Lieu, salon.Lieu)},
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