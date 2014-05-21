using EPSILab.SolarSystem.Saturn.Model.Interfaces;
using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPSILab.SolarSystem.Saturn.Model
{
    /// <summary>
    /// Access cities
    /// </summary>
    class CampusDAL : IReadable<Campus>
    {
        #region Attributes

        private readonly CampusReaderClient _client = new CampusReaderClient();

        #endregion

        #region Methods

        /// <summary>
        /// Returns the city corresponding to a code
        /// </summary>
        /// <param name="code">Code of the city desired</param>
        /// <returns>The matching city</returns>
        public Task<Campus> GetAsync(int code)
        {
            var taskCompletionSource = new TaskCompletionSource<Campus>();

            _client.GetCampusCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetCampusAsync(code);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns all cities
        /// </summary>
        /// <returns>All shows</returns>
        public Task<IList<Campus>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Campus>>();

            _client.GetCampusesCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetCampusesAsync();

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