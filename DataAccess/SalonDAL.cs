using SolarSystem.Saturn.DataAccess.Interfaces;
using SolarSystem.Saturn.DataAccess.Webservice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.DataAccess
{
    public class SalonDAL : IReadableSortable<Salon>, ISearchable<Salon>
    {
        private readonly SalonReaderClient _client = new SalonReaderClient();

        public Task<Salon> GetAsync(int code)
        {
            var taskCompletionSource = new TaskCompletionSource<Salon>();

            _client.GetSalonCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetSalonAsync(code);

            return taskCompletionSource.Task;
        }

        public Task<IList<Salon>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Salon>>();

            _client.GetSalonsCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetSalonsAsync();

            return taskCompletionSource.Task;
        }

        public Task<IList<Salon>> GetAsync(int indexFirstElement, int numberOfElements)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Salon>>();

            _client.GetSalonsLimitedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetSalonsLimitedAsync(indexFirstElement, numberOfElements);

            return taskCompletionSource.Task;
        }

        public Task<IList<Salon>> GetAsync(int indexFirstElement, int numberOfElements, SortOrder order)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Salon>>();

            _client.GetSalonsSortedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetSalonsSortedAsync(indexFirstElement, numberOfElements, order);

            return taskCompletionSource.Task;
        }

        public Task<int> GetLastInsertedId()
        {
            var taskCompletionSource = new TaskCompletionSource<int>();

            _client.GetSalonLastInsertedIdCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetSalonLastInsertedIdAsync();

            return taskCompletionSource.Task;
        }

        public Task<IList<Salon>> SearchAsync(string keywords)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Salon>>();

            _client.SearchSalonsCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.SearchSalonsAsync(keywords);

            return taskCompletionSource.Task;
        }
    }
}
