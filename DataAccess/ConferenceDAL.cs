using SolarSystem.Saturn.DataAccess.Interfaces;
using SolarSystem.Saturn.DataAccess.Webservice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.DataAccess
{
    public class ConferenceDAL : IReadableWithFilter<Conference, Ville>, ISearchable<Conference>
    {
        private readonly ConferenceReaderClient _client = new ConferenceReaderClient();

        public Task<Conference> GetAsync(int code)
        {
            var taskCompletionSource = new TaskCompletionSource<Conference>();

            _client.GetConferenceCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetConferenceAsync(code);

            return taskCompletionSource.Task;
        }

        public Task<IList<Conference>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Conference>>();

            _client.GetConferencesCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetConferencesAsync();

            return taskCompletionSource.Task;
        }

        public Task<IList<Conference>> GetAsync(int indexFirstElement, int numberOfResults)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Conference>>();

            _client.GetConferencesLimitedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetConferencesLimitedAsync(indexFirstElement, numberOfResults);

            return taskCompletionSource.Task;
        }

        public Task<IList<Conference>> GetAsync(int indexFirstElement, int numberOfResults, SortOrder order)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Conference>>();

            _client.GetConferencesSortedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetConferencesSortedAsync(indexFirstElement, numberOfResults, order);

            return taskCompletionSource.Task;
        }

        public Task<IList<Conference>> GetAsync(Ville filter, int indexFirstElement, int numberOfResults, SortOrder order)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Conference>>();

            _client.GetConferencesByVilleCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetConferencesByVilleAsync(filter, indexFirstElement, numberOfResults, order);

            return taskCompletionSource.Task;
        }

        public Task<int> GetLastInsertedId()
        {
            var taskCompletionSource = new TaskCompletionSource<int>();

            _client.GetConferenceLastInsertedIdCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetConferenceLastInsertedIdAsync();

            return taskCompletionSource.Task;
        }

        public Task<IList<Conference>> SearchAsync(string keywords)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Conference>>();

            _client.SearchConferencesCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.SearchConferencesAsync(keywords);

            return taskCompletionSource.Task;
        }
    }
}
