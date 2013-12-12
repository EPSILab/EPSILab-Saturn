using SolarSystem.Saturn.DataAccess;
using SolarSystem.Saturn.DataAccess.Interfaces;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.Model.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.Model
{
    public class NewsModel : IModel<News>
    {
        public async Task<News> GetAsync(int code)
        {
            IReadable<News> dal = new NewsDAL();
            return await dal.GetAsync(code);
        }

        public async Task<IList<News>> GetAsync()
        {
            return await GetAsync(0, 0);
        }

        public async Task<IList<News>> GetAsync(int indexFirstElement, int numberOfResults)
        {
            IReadableLimitable<News> dal = new NewsDAL();
            return await dal.GetAsync(indexFirstElement, numberOfResults);
        }

        public async Task<IList<News>> SearchAsync(string keywords)
        {
            ISearchable<News> dal = new NewsDAL();
            return await dal.SearchAsync(keywords);
        }
    }
}
