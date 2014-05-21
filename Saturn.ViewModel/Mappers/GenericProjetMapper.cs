using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel.Mappers.Interfaces;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using System.Collections.Generic;
using System.Linq;

namespace EPSILab.SolarSystem.Saturn.ViewModel.Mappers
{
    /// <summary>
    /// Map a (list of) project(s) to a (list of) generic item(s)
    /// </summary>
    class GenericProjectMapper : IMapper<Project> 
    {
        /// <summary>
        /// Map a project to an generic item
        /// </summary>
        /// <param name="element">Project to map</param>
        /// <returns>Corresponding generic item</returns>
        public VisualGenericItem Map(Project element)
        {
            return new VisualGenericItem
                {
                    Id = element.Id,
                    Title = element.Name,
                    Subtitle = string.Format("{0} %", element.Progression),
                    ImageUrl = element.ImageUrl,
                    Type = element.GetType().Name
                };
        }

        /// <summary>
        /// Map a list of projects to a generic items list
        /// </summary>
        /// <param name="elements">List of projects to map</param>
        /// <returns>Corresponding generic items list</returns>
        public IList<VisualGenericItem> Map(IEnumerable<Project> elements)
        {
            return elements.Select(Map).ToList();
        }
    }
}