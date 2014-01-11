using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.Win8.Resources;
using System;
using Windows.UI.Xaml.Data;

namespace SolarSystem.Saturn.Win8.Converters
{
    public sealed class SalonInformationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Salon && parameter is string)
            {
                Salon salon = value as Salon;

                if (parameter.ToString() == "StartDate")
                {
                    return string.Format(FormatsRsxAccessor.GetString("SALON_STARTDATE_FORMAT"), salon.Date_Heure_Debut);
                }
                
                if (parameter.ToString() == "EndDate")
                {
                    return string.Format(FormatsRsxAccessor.GetString("SALON_ENDDATE_FORMAT"), salon.Date_Heure_Fin);
                }

                if (parameter.ToString() == "Location")
                {
                    return string.Format(FormatsRsxAccessor.GetString("SALON_LOCATION_FORMAT"), salon.Lieu, salon.Lieu);
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