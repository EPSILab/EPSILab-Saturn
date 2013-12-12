using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.ViewModel.Formatters;
using SolarSystem.Saturn.ViewModel.Helpers;
using SolarSystem.Saturn.ViewModel.Objects;
using System.Collections.Generic;
using System.Linq;

namespace SolarSystem.Saturn.ViewModel.Mappers
{
    static class NewsToGenericItemMapper
    {
        private static VisualGenericItem Mapper(News news)
        {
            return new VisualGenericItem
            {
                Id = news.Code_News,
                Title = news.Titre,
                Subtitle = string.Format("{0}, {1} {2} {3}", DateFormatter.Format(news.Date_Heure),
                                                            AppResourcesHelper.GetString("BY"), news.Membre.Prenom, news.Membre.Nom),
                Image = news.Image,
                Type = news.GetType().Name
            };
        }

        public static IList<VisualGenericItem> Mapper(IEnumerable<News> news)
        {
            return news.Select(Mapper).ToList();
        }
    }
}