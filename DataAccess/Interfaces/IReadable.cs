using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.DataAccess.Interfaces
{
    public interface IReadable<T>
    {
        Task<T> GetAsync(int code);
        Task<IList<T>> GetAsync();
    }
}
