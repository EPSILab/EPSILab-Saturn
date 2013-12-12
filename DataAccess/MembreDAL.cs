using SolarSystem.Saturn.DataAccess.Interfaces;
using SolarSystem.Saturn.DataAccess.Webservice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.DataAccess
{
    public class MembreDAL : IReadableWith2Filter<Membre, Ville, Role>, ISearchable<Membre>
    {
        private readonly MembreReaderClient _client = new MembreReaderClient();

        public Task<Membre> GetAsync(int code)
        {
            var taskCompletionSource = new TaskCompletionSource<Membre>();

            _client.GetMembreCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetMembreAsync(code);

            return taskCompletionSource.Task;
        }

        public Task<IList<Membre>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Membre>>();

            _client.GetMembresCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetMembresAsync();

            return taskCompletionSource.Task;
        }

        public Task<IList<Membre>> GetAsync(int indexFirstElement, int numberOfResults)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Membre>>();

            _client.GetMembresLimitedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetMembresLimitedAsync(indexFirstElement, numberOfResults);

            return taskCompletionSource.Task;
        }

        public Task<IList<Membre>> GetAsync(int indexFirstElement, int numberOfResults, SortOrder order)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Membre>>();

            _client.GetMembresSortedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetMembresSortedAsync(indexFirstElement, numberOfResults, order);

            return taskCompletionSource.Task;
        }

        public Task<IList<Membre>> GetAsync(Ville filter, int indexFirstElement, int numberOfElements, SortOrder order)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Membre>>();

            _client.GetMembresByVilleAndRoleCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetMembresByVilleAndRoleAsync(filter, null, indexFirstElement, numberOfElements, order);

            return taskCompletionSource.Task;
        }

        public Task<IList<Membre>> GetAsync(Role filter, int indexFirstElement, int numberOfElements, SortOrder order)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Membre>>();

            _client.GetMembresByVilleAndRoleCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetMembresByVilleAndRoleAsync(null, filter, indexFirstElement, numberOfElements, order);

            return taskCompletionSource.Task;
        }

        public Task<IList<Membre>> GetAsync(Ville filter1, Role filter2, int indexFirstElement, int numberOfResults, SortOrder order)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Membre>>();

            _client.GetMembresByVilleAndRoleCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetMembresByVilleAndRoleAsync(filter1, filter2, indexFirstElement, numberOfResults, order);

            return taskCompletionSource.Task;
        }

        public Task<int> GetLastInsertedId()
        {
            var taskCompletionSource = new TaskCompletionSource<int>();

            _client.GetMembreLastInsertedIdCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetMembreLastInsertedIdAsync();

            return taskCompletionSource.Task;
        }

        public Task<IList<Membre>> SearchAsync(string keywords)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Membre>>();

            _client.SearchMembresCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.SearchMembresAsync(keywords);

            return taskCompletionSource.Task;
        }
    }
}
