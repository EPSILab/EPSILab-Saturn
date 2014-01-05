using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.Model.Interfaces
{
    /// <summary>
    /// Allow to do search on elements
    /// </summary>
    /// <typeparam name="T">A olarSystem Earth entity</typeparam>
    public interface ISearchable<T>
    {
        /// <summary>
        /// Search elements
        /// </summary>
        /// <param name="keywords">Keywords separated with a space</param>
        /// <returns>Matching elements</returns>
        Task<IList<T>> SearchAsync(string keywords);
    }
}