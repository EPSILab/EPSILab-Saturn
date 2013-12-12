using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.ViewModel.Objects;
using System;
using Windows.UI.Xaml.Data;

namespace SolarSystem.Saturn.Win8.Converters
{
    public sealed class ToEmailableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            EmailableObject emailableObject = null;

            if (value is News)
            {
                //News news = value as News;

                //string url = string.Format(AppResources.WEBSITE_FORMAT, AppResources.PAGE_NEWS, news.Code_News);

                //emailableObject = new EmailableObject
                //{
                //    Subject = news.Titre,
                //    Body = string.Format(AppResources.FORMAT_EMAIL_NEWS, news.Texte_Court, news.Membre.Prenom, news.Membre.Nom, url, Environment.NewLine)
                //};
            }

            return emailableObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
