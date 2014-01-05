using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.Model.Interfaces
{
    /// <summary>
    /// An interface which allows to get one element, all elements or the last inserted id from the webservice
    /// </summary>
    /// /// <typeparam name="T">A olarSystem Earth entity</typeparam>
    public interface IReadable<T>
    {
        Task<T> GetAsync(int code);
        Task<IList<T>> GetAsync();

        Task<int> GetLastInsertedId();
    }
}