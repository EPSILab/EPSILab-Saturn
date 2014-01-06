using System.Collections.Generic;
using System.Linq;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.ViewModel.Formatters;
using SolarSystem.Saturn.ViewModel.Objects;

namespace SolarSystem.Saturn.ViewModel.Mappers
{
    static class SalonToGenericItemMapper
    {
        private static VisualGenericItem Mapper(Salon salon)
        {
            return new VisualGenericItem
            {
                Id = salon.Code_Salon,
                Title = salon.Nom,
                Subtitle = DateFormatter.Format(salon.Date_Heure_Debut),
                Image = salon.Image,
                Type = salon.GetType().Name
            };
        }

        public static IList<VisualGenericItem> Mapper(IEnumerable<Salon> salons)
        {
            return salons.Select(Mapper).ToList();
        }
    }
}