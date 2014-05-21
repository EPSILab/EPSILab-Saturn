using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel.Mappers.Interfaces;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using System.Collections.Generic;
using System.Linq;

namespace EPSILab.SolarSystem.Saturn.ViewModel.Mappers
{
    /// <summary>
    /// Map a (list of) member(s) to a (list of) generic item(s)
    /// </summary>
    class GenericMemberMapper : IMapper<Member>
    {
        /// <summary>
        /// Map a member to an generic item
        /// </summary>
        /// <param name="element">Member to map</param>
        /// <returns>Corresponding generic item</returns>
        public VisualGenericItem Map(Member element)
        {
            var item = new VisualGenericItem
                {
                    Id = element.Id,
                    Title = string.Format("{0} {1}", element.FirstName, element.LastName),
                    Subtitle = string.Format("{0}, {1}", element.Campus.Place, element.Status),
                    ImageUrl = element.ImageUrl,
                    Type = element.GetType().Name
                };

            return item;
        }

        /// <summary>
        /// Map a list of members to a generic items list
        /// </summary>
        /// <param name="elements">List of members to map</param>
        /// <returns>Corresponding generic items list</returns>
        public IList<VisualGenericItem> Map(IEnumerable<Member> elements)
        {
            return elements.Select(Map).ToList();
        }
    }
}