using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.ViewModel.Objects;
using System.Collections.Generic;
using System.Linq;

namespace SolarSystem.Saturn.ViewModel.Mappers
{
    static class MembreToGenericItemMapper
    {
        private static VisualGenericItem Mapper(Membre membre)
        {
            var item = new VisualGenericItem
                {
                    Id = membre.Code_Membre,
                    Title = membre.Prenom + " " + membre.Nom,
                    Subtitle = membre.Ville.Libelle + ", " + membre.Statut,
                    Image = membre.Image,
                    Type = membre.GetType().Name
                };

            return item;
        }

        public static IList<VisualGenericItem> Mapper(IEnumerable<Membre> membres)
        {
            return membres.Select(Mapper).ToList();
        }
    }
}