using SolarSystem.Saturn.DataAccess.Webservice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.DataAccess.Interfaces
{
    public interface IReadableWith2Filter<TValue, in TKey, in TProperty> : IReadableSortable<TValue>
    {
        Task<IList<TValue>> GetAsync(TKey filter, int indexFirstElement, int numberOfElements, SortOrder order);
        Task<IList<TValue>> GetAsync(TProperty filter, int indexFirstElement, int numberOfElements, SortOrder order);
        Task<IList<TValue>> GetAsync(TKey filter1, TProperty filter2, int indexFirstElement, int numberOfElements, SortOrder order);
    }
}
