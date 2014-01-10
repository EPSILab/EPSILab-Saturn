using System.Collections.Generic;
using System.Threading.Tasks;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.Model.ReadersService;

namespace SolarSystem.Saturn.Model
{
    /// <summary>
    /// Access to conferences
    /// </summary>
    class ConferenceDAL : IReadableWithFilter<Conference, Ville>, ISearchable<Conference>
    {
        #region Attributes

        /// <summary>
        /// Webservice proxy
        /// </summary>
        private readonly ConferenceReaderClient _proxy = new ConferenceReaderClient();

        #endregion

        #region Methods

        /// <summary>
        /// Returns the conference corresponding to a code
        /// </summary>
        /// <param name="code">Code of the conference desired</param>
        /// <returns>The matching conference</returns>
        public Task<Conference> GetAsync(int code)
        {
            var taskCompletionSource = new TaskCompletionSource<Conference>();

            _proxy.GetConferenceCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetConferenceAsync(code);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns all conferences
        /// </summary>
        /// <returns>All conferences</returns>
        public Task<IList<Conference>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Conference>>();

            _proxy.GetConferencesCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetConferencesAsync();

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns a list of conferences which can be limited
        /// </summary>
        /// <param name="indexFirstElement">Index of the first element desired</param>
        /// <param name="numberOfResults">Number of results desired</param>
        /// <returns>List limited of conferences</returns>
        public Task<IList<Conference>> GetAsync(int indexFirstElement, int numberOfResults)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Conference>>();

            _proxy.GetConferencesLimitedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetConferencesLimitedAsync(indexFirstElement, numberOfResults);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns all conferences of a campus
        /// </summary>
        /// <param name="ville">The campus</param>
        /// <returns>All campus's conferences</returns>
        public Task<IList<Conference>> GetAsync(Ville ville)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Conference>>();

            _proxy.GetConferencesByVilleCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetConferencesByVilleAsync(ville);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns a list of conferences of a campus which can be limited
        /// </summary>
        /// <param name="ville">The campus</param>
        /// /// <param name="indexFirstElement">Index of the first element desired</param>
        /// <param name="numberOfResults">Number of results desired</param>
        /// <returns>All campus's conferences</returns>
        public Task<IList<Conference>> GetAsync(Ville ville, int indexFirstElement, int numberOfResults)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Conference>>();

            _proxy.GetConferencesByVilleLimitedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetConferencesByVilleLimitedAsync(ville, indexFirstElement, numberOfResults);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns the last inserted conference's Id
        /// </summary>
        /// <returns>Last inserted conference's Id</returns>
        public Task<int> GetLastInsertedId()
        {
            var taskCompletionSource = new TaskCompletionSource<int>();

            _proxy.GetConferenceLastInsertedIdCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetConferenceLastInsertedIdAsync();

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Search conferences
        /// </summary>
        /// <param name="keywords">Search keywords separated with a space</param>
        /// <returns>Matching conferences</returns>
        public Task<IList<Conference>> SearchAsync(string keywords)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Conference>>();

            _proxy.SearchConferencesCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.SearchConferencesAsync(keywords);

            return taskCompletionSource.Task;
        }

        #endregion
    }
}