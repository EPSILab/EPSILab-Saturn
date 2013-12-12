using SolarSystem.Saturn.DataAccess.Interfaces;
using SolarSystem.Saturn.DataAccess.Webservice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.DataAccess
{
    public class RoleDAL : IReadable<Role>
    {
        private readonly RoleReaderClient _client = new RoleReaderClient();

        public Task<Role> GetAsync(int code)
        {
            var taskCompletionSource = new TaskCompletionSource<Role>();

            _client.GetRoleCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetRoleAsync(code);

            return taskCompletionSource.Task;
        }

        public Task<IList<Role>> GetAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<IList<Role>>();

            _client.GetRolesCompleted += (sender, e) => taskCompletionSource.TrySetResult(e.Result);
            _client.GetRolesAsync();

            return taskCompletionSource.Task;
        }

        public Task<IList<Role>> GetAsync(int indexFirstElement, int numberOfElements)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetLastInsertedId()
        {
            throw new System.NotImplementedException();
        }
    }
}
