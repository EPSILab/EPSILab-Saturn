using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel.Formatters;
using EPSILab.SolarSystem.Saturn.ViewModel.Mappers.Interfaces;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using System.Collections.Generic;
using System.Linq;

namespace EPSILab.SolarSystem.Saturn.ViewModel.Mappers
{
    /// <summary>
    /// Map a (list of) show(s) to a (list of) generic item(s)
    /// </summary>
    class GenericShowMapper : IMapper<Show>
    {
        /// <summary>
        /// Map a show to an generic item
        /// </summary>
        /// <param name="element">Show to map</param>
        /// <returns>Corresponding generic item</returns>
        public VisualGenericItem Map(Show element)
        {
            return new VisualGenericItem
            {
                Id = element.Id,
                Title = element.Name,
                Subtitle = DateFormatter.Format(element.Start_DateTime),
                ImageUrl = element.ImageUrl,
                Type = element.GetType().Name
            };
        }

        /// <summary>
        /// Map a list of shows to a generic items list
        /// </summary>
        /// <param name="elements">List of shows to map</param>
        /// <returns>Corresponding generic items list</returns>
        public IList<VisualGenericItem> Map(IEnumerable<Show> elements)
        {
            return elements.Select(Map).ToList();
        }
    }
}