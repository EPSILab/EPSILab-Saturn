using System.Collections.Generic;
using System.Threading.Tasks;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.Model.ReadersService;

namespace SolarSystem.Saturn.Model
{
    /// <summary>
    /// Access to the news
    /// </summary>
    public class NewsDAL : IReadableLimitable<News>, ISearchable<News>
    {
        #region Attributes

        /// <summary>
        /// Webservice proxy for news
        /// </summary>
        private readonly NewsReaderClient _proxy = new NewsReaderClient();

        #endregion

        #region Methods

        /// <summary>
        /// Returns the news corresponding to a code
        /// </summary>
        /// <param name="code">Code of the news desired</param>
        /// <returns>The matching news</returns>
        public Task<News> GetAsync(int code)
        {
            var taskCompletionSource = new TaskCompletionSource<News>();

            _proxy.GetNewsCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetNewsAsync(code);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns all news
        /// </summary>
        /// <returns>All news</returns>
        public Task<IList<News>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<News>>();

            _proxy.GetListNewsCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetListNewsAsync();

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns a list of news which can be limited
        /// </summary>
        /// <param name="indexFirstElement">Index of the first element desired</param>
        /// <param name="numberOfResults">Number of results desired</param>
        /// <returns>List limited of news</returns>
        public Task<IList<News>> GetAsync(int indexFirstElement, int numberOfResults)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<News>>();

            _proxy.GetListNewsLimitedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetListNewsLimitedAsync(indexFirstElement, numberOfResults);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Returns the last inserted news's Id
        /// </summary>
        /// <returns>Last inserted news's Id</returns>
        public Task<int> GetLastInsertedId()
        {
            var taskCompletionSource = new TaskCompletionSource<int>();

            _proxy.GetNewsLastInsertedIdCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.GetNewsLastInsertedIdAsync();

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// Search news
        /// </summary>
        /// <param name="keywords">Keywords separated with a space</param>
        /// <returns>Matching news</returns>
        public Task<IList<News>> SearchAsync(string keywords)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<News>>();

            _proxy.SearchNewsCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _proxy.SearchNewsAsync(keywords);

            return taskCompletionSource.Task;
        }

        #endregion
    }
}