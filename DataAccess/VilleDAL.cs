using SolarSystem.Saturn.DataAccess.Interfaces;
using SolarSystem.Saturn.DataAccess.Webservice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.DataAccess
{
    public class VilleDAL : IReadable<Ville>
    {
        private readonly VilleReaderClient _client = new VilleReaderClient();

        public Task<Ville> GetAsync(int code)
        {
            var taskCompletionSource = new TaskCompletionSource<Ville>();

            _client.GetVilleCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetVilleAsync(code);

            return taskCompletionSource.Task;
        }

        public Task<IList<Ville>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Ville>>();

            _client.GetVillesCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetVillesAsync();

            return taskCompletionSource.Task;
        }

        public Task<IList<Ville>> GetAsync(int indexFirstElement, int numberOfElements)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetLastInsertedId()
        {
            throw new System.NotImplementedException();
        }
    }
}
