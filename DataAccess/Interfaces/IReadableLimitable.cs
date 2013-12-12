using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.DataAccess.Interfaces
{
    public interface IReadableLimitable<T> : IReadable<T>
    {
        Task<IList<T>> GetAsync(int indexFirstElement, int numberOfElements);
        Task<int> GetLastInsertedId();
    }
}
