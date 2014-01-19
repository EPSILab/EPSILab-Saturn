using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel.Formatters;
using EPSILab.SolarSystem.Saturn.ViewModel.Mappers.Interfaces;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using System.Collections.Generic;
using System.Linq;

namespace EPSILab.SolarSystem.Saturn.ViewModel.Mappers
{
    /// <summary>
    /// Map a (list of) conference(s) to a (list of) generic item(s)
    /// </summary>
    class GenericConferenceMapper : IMapper<Conference>
    {
        /// <summary>
        /// Map a conference to an generic item
        /// </summary>
        /// <param name="element">Conference to map</param>
        /// <returns>Corresponding generic item</returns>
        public VisualGenericItem Map(Conference element)
        {
            return new VisualGenericItem
            {
                Id = element.Code_Conference,
                Title = element.Nom,
                Subtitle = string.Format("{0}, {1}", element.Ville.Libelle, DateFormatter.Format(element.Date_Heure_Debut)),
                Image = element.Image,
                Type = element.GetType().Name
            };
        }

        /// <summary>
        /// Map a list of conferences to a generic items list
        /// </summary>
        /// <param name="elements">List of conferences to map</param>
        /// <returns>Corresponding generic items list</returns>
        public IList<VisualGenericItem> Map(IEnumerable<Conference> elements)
        {
            return elements.Select(Map).ToList();
        }
    }
}