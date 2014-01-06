using SolarSystem.Saturn.ViewModel.Objects;
using System.Collections.Generic;

namespace SolarSystem.Saturn.ViewModel.Mappers.Interfaces
{
    public interface IMapper<in T>
    {
        VisualGenericItem Map(T element);
        IList<VisualGenericItem> Map(IEnumerable<T> elements);
    }
}