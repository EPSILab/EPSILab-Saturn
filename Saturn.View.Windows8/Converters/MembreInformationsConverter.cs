using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.Win8.Resources;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace SolarSystem.Saturn.Win8.Converters
{
    /// <summary>
    /// A converter used by the Member details page.
    /// Display informations below the passed parameter
    /// </summary>
    public sealed class MembreInformationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Membre && parameter is string)
            {
                Membre membre = value as Membre;

                IDictionary<string, string> informations = new Dictionary<string, string>
                {
                    { "From", string.Format(FormatsRsxAccessor.GetString("Member_From"), membre.Ville_origine) },
                    { "CampusInfo", string.Format(FormatsRsxAccessor.GetString("Member_CampusInfo"), membre.Classe.Annee_Promo_Sortante, membre.Ville.Libelle) },
                    { "Name", string.Format(FormatsRsxAccessor.GetString("Member_Name"), membre.Prenom, membre.Nom) }
                };

                return informations[parameter.ToString()];
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}