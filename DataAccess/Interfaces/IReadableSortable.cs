using SolarSystem.Saturn.DataAccess.Webservice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.DataAccess.Interfaces
{
    public interface IReadableSortable<T> : IReadableLimitable<T>
    {
        Task<IList<T>> GetAsync(int indexFirstElement, int numberOfElements, SortOrder order);
    }
}
