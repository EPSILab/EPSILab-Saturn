using System.Collections.Generic;
using System.Linq;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.ViewModel.Objects;

namespace SolarSystem.Saturn.ViewModel.Mappers
{
    static class ProjetToGenericItemMapper
    {
        private static VisualGenericItem Mapper(Projet projet)
        {
            return new VisualGenericItem
                {
                    Id = projet.Code_Projet,
                    Title = projet.Nom,
                    Subtitle = string.Format("{0} %", projet.Avancement),
                    Image = projet.Image,
                    Type = projet.GetType().Name
                };
        }

        public static IList<VisualGenericItem> Mapper(IEnumerable<Projet> projets)
        {
            return projets.Select(Mapper).ToList();
        }
    }
}