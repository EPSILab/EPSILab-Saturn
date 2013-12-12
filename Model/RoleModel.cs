using SolarSystem.Saturn.DataAccess;
using SolarSystem.Saturn.DataAccess.Interfaces;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.Model.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.Model
{
    class RoleModel : IModel<Role>
    {
        public async Task<Role> GetAsync(int code)
        {
            IReadable<Role> dal = new RoleDAL();
            return await dal.GetAsync(code);
        }

        public async Task<IList<Role>> GetAsync()
        {
            IReadable<Role> dal = new RoleDAL();
            return await dal.GetAsync();
        }

        public async Task<IList<Role>> GetAsync(int indexFirstElement, int numberOfResults)
        {
            return await GetAsync();
        }

        public Task<IList<Role>> SearchAsync(string keywords)
        {
            return null;
        }
    }
}
