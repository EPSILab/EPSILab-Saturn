using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.ViewModel.Formatters;
using SolarSystem.Saturn.ViewModel.Helpers;
using SolarSystem.Saturn.ViewModel.Mappers.Interfaces;
using SolarSystem.Saturn.ViewModel.Objects;
using System.Collections.Generic;
using System.Linq;

namespace SolarSystem.Saturn.ViewModel.Mappers
{
    /// <summary>
    /// Map a (list of) member(s) to a (list of) generic item(s)
    /// </summary>
    class GenericNewsMapper : IMapper<News>
    {
        /// <summary>
        /// Map a news to an generic item
        /// </summary>
        /// <param name="element">News to map</param>
        /// <returns>Corresponding generic item</returns>
        public VisualGenericItem Map(News element)
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

        /// <summary>
        /// Map a list of news to a generic items list
        /// </summary>
        /// <param name="elements">List of news to map</param>
        /// <returns>Corresponding generic items list</returns>
        public IList<VisualGenericItem> Map(IEnumerable<News> elements)
        {
            return elements.Select(Map).ToList();
        }
    }
}