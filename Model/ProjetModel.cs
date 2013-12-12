using SolarSystem.Saturn.DataAccess;
using SolarSystem.Saturn.DataAccess.Interfaces;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.Model.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.Model
{
    class ProjetModel : IModel<Projet>
    {
        public async Task<Projet> GetAsync(int code)
        {
            IReadable<Projet> dal = new ProjetDAL();
            return await dal.GetAsync(code);
        }

        public async Task<IList<Projet>> GetAsync()
        {
            return await GetAsync(0, 0);
        }

        public async Task<IList<Projet>> GetAsync(int indexFirstElement, int numberOfResults)
        {
            IReadableLimitable<Projet> dal = new ProjetDAL();
            return await dal.GetAsync(indexFirstElement, numberOfResults);
        }

        public Task<IList<Projet>> SearchAsync(string keywords)
        {
            return null;
        }
    }
}
