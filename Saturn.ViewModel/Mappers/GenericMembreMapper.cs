using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.ViewModel.Mappers.Interfaces;
using SolarSystem.Saturn.ViewModel.Objects;
using System.Collections.Generic;
using System.Linq;

namespace SolarSystem.Saturn.ViewModel.Mappers
{
    /// <summary>
    /// Map a (list of) member(s) to a (list of) generic item(s)
    /// </summary>
    class GenericMembreMapper : IMapper<Membre>
    {
        /// <summary>
        /// Map a member to an generic item
        /// </summary>
        /// <param name="element">Member to map</param>
        /// <returns>Corresponding generic item</returns>
        public VisualGenericItem Map(Membre element)
        {
            var item = new VisualGenericItem
                {
                    Id = element.Code_Membre,
                    Title = string.Format("{0} {1}", element.Prenom, element.Nom),
                    Subtitle = string.Format("{0}, {1}", element.Ville.Libelle, element.Statut),
                    Image = element.Image,
                    Type = element.GetType().Name
                };

            return item;
        }

        /// <summary>
        /// Map a list of members to a generic items list
        /// </summary>
        /// <param name="elements">List of members to map</param>
        /// <returns>Corresponding generic items list</returns>
        public IList<VisualGenericItem> Map(IEnumerable<Membre> elements)
        {
            return elements.Select(Map).ToList();
        }
    }
}