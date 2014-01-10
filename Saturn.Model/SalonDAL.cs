using System.Collections.Generic;
using System.Threading.Tasks;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.Model.ReadersService;

namespace SolarSystem.Saturn.Model
{
    /// <summary>
    /// Access to shows
    /// </summary>
    class SalonDAL : IReadableLimitable<Salon>, ISearchable<Salon>
    {
        #region Attributes

        /// <summary>
        /// Webservice proxy for shows
        /// </summary>
        private readonly SalonReaderClient _proxy = new SalonReaderClient();

        #endregion

        #region Methods

        /// <summary>
        /// Returns the show corresponding to a code
        /// </summary>
        /// <param name="code">Code of the show desired</param>
        /// <returns>The matching show</returns>
        public Task<Salon> GetAsync(int code)
        {
            var taskCompletionSource = new TaskCompletionSource<Salon>();

            _proxy.GetSalonCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetSalonAsync(code);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns all shows
        /// </summary>
        /// <returns>All shows</returns>
        public Task<IList<Salon>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Salon>>();

            _proxy.GetSalonsCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetSalonsAsync();

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns a list of shows which can be limited
        /// </summary>
        /// <param name="indexFirstElement">Index of the first element desired</param>
        /// <param name="numberOfElements">Number of results desired</param>
        /// <returns>List limited of shows</returns>
        public Task<IList<Salon>> GetAsync(int indexFirstElement, int numberOfElements)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Salon>>();

            _proxy.GetSalonsLimitedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetSalonsLimitedAsync(indexFirstElement, numberOfElements);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns the last inserted show's Id
        /// </summary>
        /// <returns>Last inserted show's Id</returns>
        public Task<int> GetLastInsertedId()
        {
            var taskCompletionSource = new TaskCompletionSource<int>();

            _proxy.GetSalonLastInsertedIdCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetSalonLastInsertedIdAsync();

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Search shows
        /// </summary>
        /// <param name="keywords">Search keywords separated with a space</param>
        /// <returns>Matching shows</returns>
        public Task<IList<Salon>> SearchAsync(string keywords)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Salon>>();

            _proxy.SearchSalonsCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.SearchSalonsAsync(keywords);

            return taskCompletionSource.Task;
        }

        #endregion
    }
}