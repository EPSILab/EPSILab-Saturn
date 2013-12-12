using SolarSystem.Saturn.DataAccess.Webservice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.DataAccess.Interfaces
{
    public interface IReadableWithFilter<TValue, in TKey> : IReadableSortable<TValue>
    {
        Task<IList<TValue>> GetAsync(TKey filter, int indexFirstElement, int numberOfElements, SortOrder order);
    }
}
