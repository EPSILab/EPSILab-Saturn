using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.Model.Interface
{
    public interface IModel<T>
    {
        Task<T> GetAsync(int code);
        Task<IList<T>> GetAsync();
        Task<IList<T>> GetAsync(int indexFirstElement, int numberOfResults);
        Task<IList<T>> SearchAsync(string keywords);
    }
}