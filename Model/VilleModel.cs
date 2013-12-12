using SolarSystem.Saturn.DataAccess;
using SolarSystem.Saturn.DataAccess.Interfaces;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.Model.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.Model
{
    class VilleModel : IModel<Ville>
    {
        public async Task<Ville> GetAsync(int code)
        {
            IReadable<Ville> dal = new VilleDAL();
            return await dal.GetAsync(code);
        }

        public async Task<IList<Ville>> GetAsync()
        {
            IReadable<Ville> dal = new VilleDAL();
            return await dal.GetAsync();
        }

        public async Task<IList<Ville>> GetAsync(int indexFirstElement, int numberOfResults)
        {
            return await GetAsync();
        }

        public Task<IList<Ville>> SearchAsync(string keywords)
        {
            return null;
        }
    }
}
