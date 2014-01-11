using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.Win8.Resources;
using System;
using Windows.UI.Xaml.Data;

namespace SolarSystem.Saturn.Win8.Converters
{
    public sealed class NewsInformationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is News && parameter is string)
            {
                News news = value as News;

                if (parameter.ToString() == "WrittenBy")
                {
                    return string.Format(FormatsRsxAccessor.GetString("NEWS_AUTHOR_FORMAT"), news.Membre.Prenom, news.Membre.Nom);
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
