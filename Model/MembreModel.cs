using SolarSystem.Saturn.DataAccess;
using SolarSystem.Saturn.DataAccess.Interfaces;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.Model.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.Model
{
    class MembreModel : IModel<Membre>
    {
        public async Task<Membre> GetAsync(int code)
        {
            IReadable<Membre> dal = new MembreDAL();
            return await dal.GetAsync(code);
        }

        public async Task<IList<Membre>> GetAsync()
        {
            IReadable<Membre> dal = new MembreDAL();
            return await dal.GetAsync();
        }

        public async Task<IList<Membre>> GetAsync(int indexFirstElement, int numberOfResults)
        {
            IModel<Role> modelRole = new RoleModel();
            Role role = await modelRole.GetAsync(3);

            IReadableWith2Filter<Membre, Ville, Role> dal = new MembreDAL();
            return await dal.GetAsync(role, indexFirstElement, numberOfResults, SortOrder.Ascending);
        }

        public async Task<IList<Membre>> SearchAsync(string keywords)
        {
            ISearchable<Membre> dal = new MembreDAL();
            return await dal.SearchAsync(keywords);
        }
    }
}