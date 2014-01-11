using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.Win8.Resources;
using System;
using Windows.UI.Xaml.Data;

namespace SolarSystem.Saturn.Win8.Converters
{
    public sealed class MembreInformationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Membre && parameter is string)
            {
                Membre membre = value as Membre;

                if (parameter.ToString() == "From")
                {
                    return string.Format(FormatsRsxAccessor.GetString("MEMBRE_FROM_FORMAT"), membre.Ville_origine);
                }

                if (parameter.ToString() == "CampusInfo")
                {
                    return string.Format(FormatsRsxAccessor.GetString("MEMBRE_CAMPUSINFO_FORMAT"), membre.Classe.Annee_Promo_Sortante, membre.Ville.Libelle);
                }

                if (parameter.ToString() == "Name")
                {
                    return string.Format(FormatsRsxAccessor.GetString("MEMBRE_NAME_FORMAT"), membre.Prenom, membre.Nom);
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
