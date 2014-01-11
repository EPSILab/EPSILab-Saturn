using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.Win8.Resources;
using System;
using Windows.UI.Xaml.Data;

namespace SolarSystem.Saturn.Win8.Converters
{
    /// <summary>
    /// Converter used for the News details page.
    /// Display show informations in terms of the parameter
    /// </summary>
    public sealed class NewsInformationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is News && parameter is string)
            {
                News news = value as News;

                if (parameter.ToString() == "Author")
                {
                    return string.Format(FormatsRsxAccessor.GetString("News_Author"), news.Membre.Prenom, news.Membre.Nom);
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}