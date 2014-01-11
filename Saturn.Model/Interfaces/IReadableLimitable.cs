using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.Model.Interfaces
{
    /// <summary>
    /// An interface which allows to get limited results from the webservice
    /// </summary>
    /// <typeparam name="TResult">A olarSystem Earth entity</typeparam>
    public interface IReadableLimitable<TResult> : IReadable<TResult>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexFirstElement"></param>
        /// <param name="numberOfElements"></param>
        /// <returns></returns>
        Task<IList<TResult>> GetAsync(int indexFirstElement, int numberOfElements);
    }
}