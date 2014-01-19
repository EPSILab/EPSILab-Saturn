using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPSILab.SolarSystem.Saturn.Model.Interfaces
{
    /// <summary>
    /// An interface which allows to get elements with a filter from the webservice
    /// </summary>
    /// <typeparam name="TResult">The SolarSystem Earth entity return type</typeparam>
    /// <typeparam name="TFilter">The SolarSystem Earth entity filter type</typeparam>
    public interface IReadableWithFilter<TResult, in TFilter> : IReadableLimitable<TResult>
    {
        Task<IList<TResult>> GetAsync(TFilter filter);

        Task<IList<TResult>> GetAsync(TFilter filter, int indexFirstElement, int numberOfResults);
    }
}