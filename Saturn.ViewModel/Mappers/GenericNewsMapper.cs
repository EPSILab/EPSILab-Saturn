using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.ViewModel.Formatters;
using SolarSystem.Saturn.ViewModel.Helpers;
using SolarSystem.Saturn.ViewModel.Objects;
using System.Collections.Generic;
using System.Linq;

namespace SolarSystem.Saturn.ViewModel.Mappers
{
    static class GenericNewsMapper
    {
        private static VisualGenericItem Mapper(News element)
        {
            return new VisualGenericItem
            {
                Id = element.Code_News,
                Title = element.Titre,
                Subtitle = string.Format("{0}, {1} {2} {3}", DateFormatter.Format(element.Date_Heure),
                                                            AppResourcesHelper.GetString("BY"),
                                                            element.Membre.Prenom,
                                                            element.Membre.Nom),
                Image = element.Image,
                Type = element.GetType().Name
            };
        }

        public static IList<VisualGenericItem> Mapper(IEnumerable<News> news)
        {
            return news.Select(Mapper).ToList();
        }
    }
}