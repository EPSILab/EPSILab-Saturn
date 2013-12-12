using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.ViewModel.Objects;
using SolarSystem.Saturn.WP8.Resources;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SolarSystem.Saturn.WP8.Converters
{
    public class ToEmailableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EmailableObject emailableObject = null;

            if (value is News)
            {
                News news = value as News;

                string url = string.Format(AppResources.WEBSITE_FORMAT, AppResources.PAGE_NEWS, news.Code_News, news.URL);

                emailableObject = new EmailableObject
                {
                    Subject = news.Titre,
                    Body = string.Format(AppResources.FORMAT_EMAIL_NEWS, news.Texte_Court, news.Membre.Prenom, news.Membre.Nom, url, Environment.NewLine)
                };
            }

            return emailableObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
