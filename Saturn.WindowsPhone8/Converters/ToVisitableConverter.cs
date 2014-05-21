using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.WindowsPhone8.Resources;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EPSILab.SolarSystem.Saturn.WindowsPhone8.Converters
{
    public class ToVisitableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string url = null;

            if (value is News)
            {
                News news = value as News;
                url = string.Format(AppResources.WEBSITE_FORMAT, AppResources.PAGE_NEWS, news.Id, news.Url);
            }
            else if (value is Conference)
            {
                Conference conference = value as Conference;
                url = string.Format(AppResources.WEBSITE_FORMAT, AppResources.PAGE_CONFERENCES, conference.Id, conference.Url);
            }
            else if (value is Show)
            {
                Show salon = value as Show;
                url = string.Format(AppResources.WEBSITE_FORMAT, AppResources.PAGE_SALONS, salon.Id, salon.Url);
            }
            else if (value is Member)
            {
                Member member = value as Member;
                url = string.Format(AppResources.WEBSITE_FORMAT, AppResources.PAGE_MEMBRES, member.Id, member.Url);
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