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
                    Title = news.Titre,
                    Message = news.Texte_Court,
                    Uri = new Uri(string.Format(AppResources.WEBSITE_FORMAT, AppResources.PAGE_NEWS, news.Code_News, news.URL))
                };
            }
            else if (value is Conference)
            {
                Conference conference = value as Conference;

                shareableObject = new ShareableObject
                {
                    Title = conference.Nom,
                    Message = string.Format(AppResources.FORMAT_CONFERENCE, conference.Date_Heure_Debut, conference.Date_Heure_Fin, conference.Lieu),
                    Uri = new Uri(string.Format(AppResources.WEBSITE_FORMAT, AppResources.PAGE_CONFERENCES, conference.Code_Conference, conference.URL))
                };
            }
            else if (value is Salon)
            {
                Salon salon = value as Salon;

                shareableObject = new ShareableObject
                {
                    Title = salon.Nom,
                    Message = string.Format(AppResources.FORMAT_SALON, salon.Date_Heure_Debut, salon.Date_Heure_Fin, salon.Lieu),
                    Uri = new Uri(string.Format(AppResources.WEBSITE_FORMAT, AppResources.PAGE_SALONS, salon.Code_Salon, salon.URL))
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
