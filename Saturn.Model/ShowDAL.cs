using EPSILab.SolarSystem.Saturn.Model.Interfaces;
using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPSILab.SolarSystem.Saturn.Model
{
    /// <summary>
    /// Access to shows
    /// </summary>
    class ShowDAL : IReadableLimitable<Show>, ISearchable<Show>
    {
        #region Attributes

        /// <summary>
        /// Webservice proxy for shows
        /// </summary>
        private readonly ShowReaderClient _proxy = new ShowReaderClient();

        #endregion

        #region Methods

        /// <summary>
        /// Returns the show corresponding to a code
        /// </summary>
        /// <param name="code">Code of the show desired</param>
        /// <returns>The matching show</returns>
        public Task<Show> GetAsync(int code)
        {
            var taskCompletionSource = new TaskCompletionSource<Show>();

            _proxy.GetShowCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetShowAsync(code);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns all shows
        /// </summary>
        /// <returns>All shows</returns>
        public Task<IList<Show>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Show>>();

            _proxy.GetShowsCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetShowsAsync();

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns a list of shows which can be limited
        /// </summary>
        /// <param name="indexFirstElement">Index of the first element desired</param>
        /// <param name="numberOfElements">Number of results desired</param>
        /// <returns>List limited of shows</returns>
        public Task<IList<Show>> GetAsync(int indexFirstElement, int numberOfElements)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Show>>();

            _proxy.GetShowsLimitedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetShowsLimitedAsync(indexFirstElement, numberOfElements);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns the last inserted show's Id
        /// </summary>
        /// <returns>Last inserted show's Id</returns>
        public Task<int> GetLastInsertedId()
        {
            var taskCompletionSource = new TaskCompletionSource<int>();

            _proxy.GetShowLastInsertedIdCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetShowLastInsertedIdAsync();

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Search shows
        /// </summary>
        /// <param name="keywords">Search keywords separated with a space</param>
        /// <returns>Matching shows</returns>
        public Task<IList<Show>> SearchAsync(string keywords)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Show>>();

            _proxy.SearchShowsCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.SearchShowsAsync(keywords);

            return taskCompletionSource.Task;
        }

        #endregion
    }
}