using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.ViewModel.Objects;
using SolarSystem.Saturn.WP8.Resources;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SolarSystem.Saturn.WP8.Converters
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
                    Id = string.Format("{0}-News-{1}", AppResources.APPLICATION_NAME, news.Code_News),
                    NavigationPage = new Uri(string.Format("/NewsPage.xaml?Id={0}", news.Code_News), UriKind.Relative),
                    Title = news.Titre,
                    BackTitle = news.Date_Heure.ToString("g", CultureInfo.CurrentUICulture),
                    Image = news.Image,
                    Content = news.Texte_Court
                };
            }
            else if (value is Conference)
            {
                Conference conference = value as Conference;

                pinnableObject = new PinnableObjectWP
                {
                    Id = string.Format("{0}-Conference-{1}", AppResources.APPLICATION_NAME, conference.Code_Conference),
                    NavigationPage = new Uri(string.Format("/ConferencePage.xaml?Id={0}", conference.Code_Conference), UriKind.Relative),
                    Title = conference.Nom,
                    BackTitle = string.Format(CultureInfo.CurrentUICulture, AppResources.FORMAT_CONFERENCE, conference.Date_Heure_Debut, conference.Date_Heure_Fin, conference.Lieu),
                    Image = conference.Image,
                    Content = conference.Nom
                };
            }
            else if (value is Salon)
            {
                Salon salon = value as Salon;

                pinnableObject = new PinnableObjectWP
                {
                    Id = string.Format("{0}-Salon-{1}", AppResources.APPLICATION_NAME, salon.Code_Salon),
                    NavigationPage = new Uri(string.Format("/SalonPage.xaml?Id={0}", salon.Code_Salon), UriKind.Relative),
                    Title = salon.Nom,
                    BackTitle = string.Format(CultureInfo.CurrentUICulture, AppResources.FORMAT_SALON, salon.Date_Heure_Debut, salon.Date_Heure_Fin, salon.Lieu),
                    Image = salon.Image,
                    Content = salon.Nom
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
