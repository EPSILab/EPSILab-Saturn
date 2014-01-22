using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.Windows8.Resources;
using System;
using Windows.UI.Xaml.Data;

namespace EPSILab.SolarSystem.Saturn.Windows8.Converters
{
    /// <summary>
    /// A converter which generate a Model entity to URL to be displayed on the website
    /// </summary>
    public sealed class ToVisitableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string url = null;
            string websiteFormat = FormatsRsxAccessor.GetString("Website");

            if (value is News)
            {
                News news = value as News;
                url = string.Format(websiteFormat, FormatsRsxAccessor.GetString("Page_News"), news.Code_News, news.URL);
            }
            else if (value is Conference)
            {
                Conference conference = value as Conference;
                url = string.Format(websiteFormat, FormatsRsxAccessor.GetString("Page_Conferences"), conference.Code_Conference, conference.URL);
            }
            else if (value is Salon)
            {
                Salon salon = value as Salon;
                url = string.Format(websiteFormat, FormatsRsxAccessor.GetString("Page_Shows"), salon.Code_Salon, salon.URL);
            }
            else if (value is Membre)
            {
                Membre membre = value as Membre;
                url = string.Format(websiteFormat, FormatsRsxAccessor.GetString("Page_Members"), membre.Code_Membre, membre.URL);
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