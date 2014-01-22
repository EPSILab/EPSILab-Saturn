using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPSILab.SolarSystem.Saturn.Model.Interfaces
{
    /// <summary>
    /// An interface which allows to get one element, all elements or the last inserted id from the webservice
    /// </summary>
    /// /// <typeparam name="TResult">A olarSystem Earth entity</typeparam>
    public interface IReadable<TResult>
    {
        Task<TResult> GetAsync(int code);

        Task<IList<TResult>> GetAsync();

        Task<int> GetLastInsertedId();
    }
}