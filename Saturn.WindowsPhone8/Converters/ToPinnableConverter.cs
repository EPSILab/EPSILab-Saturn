using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using EPSILab.SolarSystem.Saturn.WindowsPhone8.Resources;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EPSILab.SolarSystem.Saturn.WindowsPhone8.Converters
{
    public class ToPinnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PinnableObject pinnableObject = null;

            if (value is News)
            {
                News news = value as News;

                pinnableObject = new PinnableObjectWP
                {
                    Id = string.Format("{0}-News-{1}", AppResources.APPLICATION_NAME, news.Id),
                    NavigationPage = new Uri(string.Format("/NewsPage.xaml?Id={0}", news.Id), UriKind.Relative),
                    Title = news.Title,
                    BackTitle = news.DateTime.ToString("g", CultureInfo.CurrentUICulture),
                    ImageUrl = news.ImageUrl,
                    Content = news.ShortText
                };
            }
            else if (value is Conference)
            {
                Conference conference = value as Conference;

                pinnableObject = new PinnableObjectWP
                {
                    Id = string.Format("{0}-Conference-{1}", AppResources.APPLICATION_NAME, conference.Id),
                    NavigationPage = new Uri(string.Format("/ConferencePage.xaml?Id={0}", conference.Id), UriKind.Relative),
                    Title = conference.Name,
                    BackTitle = string.Format(CultureInfo.CurrentUICulture, AppResources.FORMAT_CONFERENCE, conference.Start_DateTime, conference.End_DateTime, conference.Place),
                    ImageUrl = conference.ImageUrl,
                    Content = conference.Name
                };
            }
            else if (value is Show)
            {
                Show salon = value as Show;

                pinnableObject = new PinnableObjectWP
                {
                    Id = string.Format("{0}-Show-{1}", AppResources.APPLICATION_NAME, salon.Id),
                    NavigationPage = new Uri(string.Format("/ShowPage.xaml?Id={0}", salon.Id), UriKind.Relative),
                    Title = salon.Name,
                    BackTitle = string.Format(CultureInfo.CurrentUICulture, AppResources.FORMAT_SALON, salon.Start_DateTime, salon.End_DateTime, salon.Place),
                    ImageUrl = salon.ImageUrl,
                    Content = salon.Name
                };
            }

            return pinnableObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}