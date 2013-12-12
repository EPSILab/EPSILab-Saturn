using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.DataAccess.Interfaces
{
    public interface ISearchable<T>
    {
        Task<IList<T>> SearchAsync(string keywords);
    }
}
