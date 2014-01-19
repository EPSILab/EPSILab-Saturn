using System.Collections.Generic;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;

namespace EPSILab.SolarSystem.Saturn.ViewModel.Mappers.Interfaces
{
    public interface IMapper<in T>
    {
        VisualGenericItem Map(T element);
        IList<VisualGenericItem> Map(IEnumerable<T> elements);
    }
}