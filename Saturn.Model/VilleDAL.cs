using System.Collections.Generic;
using System.Threading.Tasks;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.Model.ReadersService;

namespace SolarSystem.Saturn.Model
{
    /// <summary>
    /// Access cities
    /// </summary>
    class VilleDAL : IReadable<Ville>
    {
        #region Attributes

        private readonly VilleReaderClient _client = new VilleReaderClient();

        #endregion

        #region Methods

        /// <summary>
        /// Returns the city corresponding to a code
        /// </summary>
        /// <param name="code">Code of the city desired</param>
        /// <returns>The matching city</returns>
        public Task<Ville> GetAsync(int code)
        {
            var taskCompletionSource = new TaskCompletionSource<Ville>();

            _client.GetVilleCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetVilleAsync(code);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns all cities
        /// </summary>
        /// <returns>All shows</returns>
        public Task<IList<Ville>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Ville>>();

            _client.GetVillesCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetVillesAsync();

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns the last inserted show's Id
        /// </summary>
        /// <returns>Last inserted show's Id</returns>
        public Task<int> GetLastInsertedId()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}