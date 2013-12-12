using SolarSystem.Saturn.DataAccess.Interfaces;
using SolarSystem.Saturn.DataAccess.Webservice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.DataAccess
{
    public class NewsDAL : IReadableSortable<News>, ISearchable<News>
    {
        private readonly NewsReaderClient _client = new NewsReaderClient();

        public Task<News> GetAsync(int code)
        {
            var taskCompletionSource = new TaskCompletionSource<News>();

            _client.GetNewsCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetNewsAsync(code);

            return taskCompletionSource.Task;
        }

        public Task<IList<News>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<News>>();

            _client.GetListNewsCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetListNewsAsync();

            return taskCompletionSource.Task;
        }

        public Task<IList<News>> GetAsync(int indexFirstElement, int numberOfResults)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<News>>();

            _client.GetListNewsLimitedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetListNewsLimitedAsync(indexFirstElement, numberOfResults);

            return taskCompletionSource.Task;
        }

        public Task<IList<News>> GetAsync(int indexFirstElement, int numberOfResults, SortOrder order)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<News>>();

            _client.GetListNewsSortedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetListNewsSortedAsync(indexFirstElement, numberOfResults, order);

            return taskCompletionSource.Task;
        }

        public Task<int> GetLastInsertedId()
        {
            var taskCompletionSource = new TaskCompletionSource<int>();

            _client.GetNewsLastInsertedIdCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetNewsLastInsertedIdAsync();

            return taskCompletionSource.Task;
        }

        public Task<IList<News>> SearchAsync(string motsCles)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<News>>();

            _client.SearchNewsCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.SearchNewsAsync(motsCles);

            return taskCompletionSource.Task;
        }
    }
}
