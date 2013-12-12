using SolarSystem.Saturn.DataAccess;
using SolarSystem.Saturn.DataAccess.Interfaces;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.Model.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.Model
{
    class SalonModel : IModel<Salon>
    {
        public async Task<Salon> GetAsync(int code)
        {
            IReadable<Salon> dal = new SalonDAL();
            return await dal.GetAsync(code);
        }

        public async Task<IList<Salon>> GetAsync()
        {
            return await GetAsync(0, 0);
        }

        public async Task<IList<Salon>> GetAsync(int indexFirstElement, int numberOfResults)
        {
            IReadableLimitable<Salon> dal = new SalonDAL();
            return await dal.GetAsync(indexFirstElement, numberOfResults);
        }
            
        public async Task<IList<Salon>> SearchAsync(string keywords)
        {
            ISearchable<Salon> dal = new SalonDAL();
            return await dal.SearchAsync(keywords);
        }
    }
}