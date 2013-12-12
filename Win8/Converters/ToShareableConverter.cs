using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.ViewModel.Objects;
using SolarSystem.Saturn.Win8.Resources;
using System;
using Windows.UI.Xaml.Data;

namespace SolarSystem.Saturn.Win8.Converters
{
    public sealed class ToShareableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ShareableWin8Object shareableObject = null;
            string websiteFormat = FormatsRsxAccessor.GetString("WEBSITE_FORMAT");

            if (value is News)
            {
                News news = value as News;

                shareableObject = new ShareableWin8Object
                {
                    Title = news.Titre,
                    Message = string.Format(FormatsRsxAccessor.GetString("NEWS_FORMAT"), news.Titre, news.Date_Heure, news.Membre.Prenom, news.Membre.Nom),
                    Uri = new Uri(string.Format(websiteFormat, FormatsRsxAccessor.GetString("PAGE_NEWS"), news.Code_News, news.URL))
                };

                shareableObject.HTMLText = string.Format(FormatsRsxAccessor.GetString("NEWS_FORMAT_HTML"),
                                                         news.Membre.Prenom, news.Membre.Nom, news.Date_Heure,
                                                         news.Image, news.Texte_Long, shareableObject.Uri);
            }
            else if (value is Conference)
            {
                Conference conference = value as Conference;

                shareableObject = new ShareableWin8Object
                {
                    Title = conference.Nom,
                    Message = string.Format(FormatsRsxAccessor.GetString("CONFERENCE_FORMAT"), conference.Nom, conference.Date_Heure_Debut, conference.Date_Heure_Fin, conference.Lieu),
                    Uri = new Uri(string.Format(websiteFormat, FormatsRsxAccessor.GetString("PAGE_CONFERENCES"), conference.Code_Conference, conference.URL))
                };

                shareableObject.HTMLText = string.Format(FormatsRsxAccessor.GetString("CONFERENCE_FORMAT_HTML"),
                                                         conference.Date_Heure_Debut, conference.Date_Heure_Fin,
                                                         conference.Lieu,
                                                         conference.Ville.Libelle, conference.Image,
                                                         conference.Description, shareableObject.Uri);
            }
            else if (value is Salon)
            {
                Salon salon = value as Salon;

                shareableObject = new ShareableWin8Object
                {
                    Title = salon.Nom,
                    Message = string.Format(FormatsRsxAccessor.GetString("SALON_FORMAT"), salon.Nom, salon.Date_Heure_Debut, salon.Date_Heure_Fin, salon.Lieu),
                    Uri = new Uri(string.Format(websiteFormat, FormatsRsxAccessor.GetString("PAGE_SALONS"), salon.Code_Salon, salon.URL))
                };

                shareableObject.HTMLText = string.Format(FormatsRsxAccessor.GetString("SALON_FORMAT_HTML"),
                                                         salon.Date_Heure_Debut, salon.Date_Heure_Fin, salon.Lieu,
                                                         salon.Lieu,
                                                         salon.Image, salon.Description, shareableObject.Uri);
            }

            return shareableObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}