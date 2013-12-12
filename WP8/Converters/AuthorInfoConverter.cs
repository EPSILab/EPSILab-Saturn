﻿using System;
using System.Globalization;
using System.Windows.Data;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.WP8.Resources;

namespace SolarSystem.Saturn.WP8.Converters
{
    public class AuthorInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Membre)
            {
                Membre membre = value as Membre;
                return string.Format(CultureInfo.CurrentUICulture, AppResources.FORMAT_AUTHOR, membre.Prenom, membre.Nom);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}