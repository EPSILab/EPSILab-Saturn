using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.ViewModel.Formatters;
using SolarSystem.Saturn.ViewModel.Mappers.Interfaces;
using SolarSystem.Saturn.ViewModel.Objects;
using System.Collections.Generic;
using System.Linq;

namespace SolarSystem.Saturn.ViewModel.Mappers
{
    /// <summary>
    /// Map a (list of) show(s) to a (list of) generic item(s)
    /// </summary>
    class GenericSalonMapper : IMapper<Salon>
    {
        /// <summary>
        /// Map a show to an generic item
        /// </summary>
        /// <param name="element">Show to map</param>
        /// <returns>Corresponding generic item</returns>
        public VisualGenericItem Map(Salon element)
        {
            return new VisualGenericItem
            {
                Id = element.Code_Salon,
                Title = element.Nom,
                Subtitle = DateFormatter.Format(element.Date_Heure_Debut),
                Image = element.Image,
                Type = element.GetType().Name
            };
        }

        /// <summary>
        /// Map a list of shows to a generic items list
        /// </summary>
        /// <param name="elements">List of shows to map</param>
        /// <returns>Corresponding generic items list</returns>
        public IList<VisualGenericItem> Map(IEnumerable<Salon> elements)
        {
            return elements.Select(Map).ToList();
        }
    }
}