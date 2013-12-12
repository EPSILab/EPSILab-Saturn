﻿using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.WP8.Resources;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SolarSystem.Saturn.WP8.Converters
{
    public class SalonInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Salon)
            {
                Salon salon = value as Salon;
                return string.Format(CultureInfo.CurrentUICulture, AppResources.FORMAT_SALON, salon.Date_Heure_Debut, salon.Date_Heure_Fin, salon.Lieu);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}