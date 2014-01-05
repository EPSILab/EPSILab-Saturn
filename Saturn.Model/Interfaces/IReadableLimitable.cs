using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.Model.Interfaces
{
    /// <summary>
    /// An interface which allows to get limited results from the webservice
    /// </summary>
    /// <typeparam name="T">A olarSystem Earth entity</typeparam>
    public interface IReadableLimitable<T> : IReadable<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexFirstElement"></param>
        /// <param name="numberOfElements"></param>
        /// <returns></returns>
        Task<IList<T>> GetAsync(int indexFirstElement, int numberOfElements);
    }
}
