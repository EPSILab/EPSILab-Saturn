using SolarSystem.Saturn.DataAccess;
using SolarSystem.Saturn.DataAccess.Interfaces;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.Model.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.Model
{
    class ConferenceModel : IModel<Conference>
    {
        public async Task<Conference> GetAsync(int code)
        {
            IReadable<Conference> dal = new ConferenceDAL();
            return await dal.GetAsync(code);
        }

        public async Task<IList<Conference>> GetAsync()
        {
            return await GetAsync(0, 0);
        }

        public async Task<IList<Conference>> GetAsync(int indexFirstElement, int numberOfResults)
        {
            IReadableLimitable<Conference> dal = new ConferenceDAL();
            return await dal.GetAsync(indexFirstElement, numberOfResults);
        }

        public async Task<IList<Conference>> SearchAsync(string keywords)
        {
            ISearchable<Conference> dal = new ConferenceDAL();
            return await dal.SearchAsync(keywords);
        }
    }
}
