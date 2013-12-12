using SolarSystem.Saturn.DataAccess.Interfaces;
using SolarSystem.Saturn.DataAccess.Webservice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.DataAccess
{
    public class ProjetDAL : IReadableWithFilter<Projet, Ville>
    {
        private readonly ProjetReaderClient _client = new ProjetReaderClient();

        public Task<Projet> GetAsync(int code)
        {
            var taskCompletionSource = new TaskCompletionSource<Projet>();

            _client.GetProjetCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetProjetAsync(code);

            return taskCompletionSource.Task;
        }

        public Task<IList<Projet>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Projet>>();

            _client.GetProjetsCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetProjetsAsync();

            return taskCompletionSource.Task;
        }

        public Task<IList<Projet>> GetAsync(int indexFirstElement, int numberOfElements)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Projet>>();

            _client.GetProjetsLimitedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetProjetsLimitedAsync(indexFirstElement, numberOfElements);

            return taskCompletionSource.Task;
        }

        public Task<IList<Projet>> GetAsync(int indexFirstElement, int numberOfElements, SortOrder order)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Projet>>();

            _client.GetProjetsSortedCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetProjetsSortedAsync(indexFirstElement, numberOfElements, order);

            return taskCompletionSource.Task;
        }

        public Task<IList<Projet>> GetAsync(Ville filter, int indexFirstElement, int numberOfElements, SortOrder order)
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Projet>>();

            _client.GetProjetsByVilleCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetProjetsByVilleAsync(filter, indexFirstElement, numberOfElements, order);

            return taskCompletionSource.Task;
        }

        public Task<int> GetLastInsertedId()
        {
            var taskCompletionSource = new TaskCompletionSource<int>();

            _client.GetProjetLastInsertedIdCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetProjetLastInsertedIdAsync();

            return taskCompletionSource.Task;
        }
    }
}
