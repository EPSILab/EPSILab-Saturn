using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.Windows8.Resources;
using System;
using Windows.UI.Xaml.Data;

namespace EPSILab.SolarSystem.Saturn.Windows8.Converters
{
    /// <summary>
    /// A converter which generate a Model entity to Url to be displayed on the website
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
                url = string.Format(websiteFormat, FormatsRsxAccessor.GetString("Page_News"), news.Id, news.Url);
            }
            else if (value is Conference)
            {
                Conference conference = value as Conference;
                url = string.Format(websiteFormat, FormatsRsxAccessor.GetString("Page_Conferences"), conference.Id, conference.Url);
            }
            else if (value is Show)
            {
                Show salon = value as Show;
                url = string.Format(websiteFormat, FormatsRsxAccessor.GetString("Page_Shows"), salon.Id, salon.Url);
            }
            else if (value is Member)
            {
                Member member = value as Member;
                url = string.Format(websiteFormat, FormatsRsxAccessor.GetString("Page_Members"), member.Id, member.Url);
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