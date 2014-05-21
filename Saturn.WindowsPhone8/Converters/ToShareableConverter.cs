using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using EPSILab.SolarSystem.Saturn.WindowsPhone8.Resources;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EPSILab.SolarSystem.Saturn.WindowsPhone8.Converters
{
    public class ToShareableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ShareableObject shareableObject = null;

            if (value is News)
            {
                News news = value as News;

                shareableObject = new ShareableObject
                {
                    Title = news.Title,
                    Message = news.ShortText,
                    Uri = new Uri(string.Format(AppResources.WEBSITE_FORMAT, AppResources.PAGE_NEWS, news.Id, news.Url))
                };
            }
            else if (value is Conference)
            {
                Conference conference = value as Conference;

                shareableObject = new ShareableObject
                {
                    Title = conference.Name,
                    Message = string.Format(AppResources.FORMAT_CONFERENCE, conference.Start_DateTime, conference.End_DateTime, conference.Place),
                    Uri = new Uri(string.Format(AppResources.WEBSITE_FORMAT, AppResources.PAGE_CONFERENCES, conference.Id, conference.Url))
                };
            }
            else if (value is Show)
            {
                Show salon = value as Show;

                shareableObject = new ShareableObject
                {
                    Title = salon.Name,
                    Message = string.Format(AppResources.FORMAT_SALON, salon.Start_DateTime, salon.End_DateTime, salon.Place),
                    Uri = new Uri(string.Format(AppResources.WEBSITE_FORMAT, AppResources.PAGE_SALONS, salon.Id, salon.Url))
                };
            }

            return shareableObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
