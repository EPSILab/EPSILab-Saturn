using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.View.WindowsPhone.Resources;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SolarSystem.Saturn.View.WindowsPhone.Converters
{
    public class ToVisitableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string url = null;

            if (value is News)
            {
                News news = value as News;
                url = string.Format(AppResources.WEBSITE_FORMAT, AppResources.PAGE_NEWS, news.Code_News, news.URL);
            }
            else if (value is Conference)
            {
                Conference conference = value as Conference;
                url = string.Format(AppResources.WEBSITE_FORMAT, AppResources.PAGE_CONFERENCES, conference.Code_Conference, conference.URL);
            }
            else if (value is Salon)
            {
                Salon salon = value as Salon;
                url = string.Format(AppResources.WEBSITE_FORMAT, AppResources.PAGE_SALONS, salon.Code_Salon, salon.URL);
            }
            else if (value is Membre)
            {
                Membre membre = value as Membre;
                url = string.Format(AppResources.WEBSITE_FORMAT, AppResources.PAGE_MEMBRES, membre.Code_Membre, membre.URL);
            }

            Uri uri = null;

            if (url != null)
            {
                uri = new Uri(url);
            }

            return uri;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}