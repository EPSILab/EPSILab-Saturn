using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.Win8.Resources;
using System;
using Windows.UI.Xaml.Data;

namespace SolarSystem.Saturn.Win8.Converters
{
    public sealed class ProjetInformationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Projet && parameter is string)
            {
                Projet projet = value as Projet;

                if (parameter.ToString() == "Progress")
                {
                    return string.Format(FormatsRsxAccessor.GetString("PROJET_PROGRESS_FORMAT"), projet.Avancement);
                }

                if (parameter.ToString() == "Location")
                {
                    return string.Format(FormatsRsxAccessor.GetString("PROJET_LOCATION_FORMAT"), projet.Ville.Libelle);
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