using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.Model.Interfaces
{
    /// <summary>
    /// Allow to do search on elements
    /// </summary>
    /// <typeparam name="TResult">A olarSystem Earth entity</typeparam>
    public interface ISearchable<TResult>
    {
        /// <summary>
        /// Search elements
        /// </summary>
        /// <param name="keywords">Keywords separated with a space</param>
        /// <returns>Matching elements</returns>
        Task<IList<TResult>> SearchAsync(string keywords);
    }
}