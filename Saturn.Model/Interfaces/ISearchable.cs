using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPSILab.SolarSystem.Saturn.Model.Interfaces
{
    /// <summary>
    /// Allow to do search on elements
    /// </summary>
    /// <typeparam name="TResult">A olarSystem Earth entity</typeparam>
    public interface ISearchable<TResult>
    {
        Task<IList<TResult>> SearchAsync(string keywords);
    }
}