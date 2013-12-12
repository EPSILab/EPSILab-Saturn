using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.ViewModel.Formatters;
using SolarSystem.Saturn.ViewModel.Objects;
using System.Collections.Generic;
using System.Linq;

namespace SolarSystem.Saturn.ViewModel.Mappers
{
    static class ConferenceToGenericItemMapper
    {
        private static VisualGenericItem Mapper(Conference conference)
        {
            return new VisualGenericItem
            {
                Id = conference.Code_Conference,
                Title = conference.Nom,
                Subtitle = conference.Ville.Libelle + ", " + DateFormatter.Format(conference.Date_Heure_Debut),
                Image = conference.Image,
                Type = conference.GetType().Name
            };
        }

        public static IList<VisualGenericItem> Mapper(IEnumerable<Conference> conferences)
        {
            return conferences.Select(Mapper).ToList();
        }
    }
}