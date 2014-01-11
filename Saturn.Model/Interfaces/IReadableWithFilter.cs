using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.Model.Interfaces
{
    /// <summary>
    /// An interface which allows to get elements with a filter from the webservice
    /// </summary>
    /// <typeparam name="TResult">The SolarSystem Earth entity return type</typeparam>
    /// <typeparam name="TFilter">The SolarSystem Earth entity filter type</typeparam>
    public interface IReadableWithFilter<TResult, in TFilter> : IReadableLimitable<TResult>
    {
        /// <summary>
        /// Get the elements corresponding to the filter
        /// </summary>
        /// <param name="filter">The filter</param>
        /// <returns>Matching results</returns>
        Task<IList<TResult>> GetAsync(TFilter filter);

        /// <summary>
        /// Get a list of elements corresponding to the filter which can be limited
        /// </summary>
        /// <param name="filter">The filter</param>
        /// <param name="indexFirstElement">Index of the first result desired</param>
        /// <param name="numberOfResults">Number of results desired</param>
        /// <returns>List limited of matching results</returns>
        Task<IList<TResult>> GetAsync(TFilter filter, int indexFirstElement, int numberOfResults);
    }
}