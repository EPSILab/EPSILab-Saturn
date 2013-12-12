using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.Win8.Resources;
using System;
using Windows.UI.Xaml.Data;

namespace SolarSystem.Saturn.Win8.Converters
{
    public sealed class ToVisitableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string url = null;
            string websiteFormat = FormatsRsxAccessor.GetString("WEBSITE_FORMAT");

            if (value is News)
            {
                News news = value as News;
                url = string.Format(websiteFormat, FormatsRsxAccessor.GetString("PAGE_NEWS"), news.Code_News, news.URL);
            }
            else if (value is Conference)
            {
                Conference conference = value as Conference;
                url = string.Format(websiteFormat, FormatsRsxAccessor.GetString("PAGE_CONFERENCES"), conference.Code_Conference, conference.URL);
            }
            else if (value is Salon)
            {
                Salon salon = value as Salon;
                url = string.Format(websiteFormat, FormatsRsxAccessor.GetString("PAGE_SALONS"), salon.Code_Salon, salon.URL);
            }
            else if (value is Membre)
            {
                Membre membre = value as Membre;
                url = string.Format(websiteFormat, FormatsRsxAccessor.GetString("PAGE_MEMBRES"), membre.Code_Membre, membre.URL);
            }

            Uri uri = null;

            if (url != null)
            {
                uri = new Uri(url);
            }

            return uri;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}